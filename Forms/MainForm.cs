using System;
using System.Data;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using JobData;
using EventHook;
using EventHook.Tools;
using JobRunner;

namespace AutomatedWorker.Forms
{
    public partial class MainForm : Form
    {
        #region Attributes
        private readonly ApplicationWatcher applicationWatcher;
        private readonly EventHookFactory eventHookFactory = new EventHookFactory();
        private readonly KeyboardWatcher keyboardWatcher;
        private readonly ClipboardWatcher clipboardWatcher;

        private readonly MouseWatcher mouseWatcher;
        private Mouse.MousePoint mousePoint;
        private ScreenshotMaker screenshotMaker;
        private Runner runner;

        // private readonly PrintWatcher printWatcher;
        private Config config;
        private Job job;
        private JobManager jobManager;
        private LoadJobForm jobForm;
        private ObjectManager objectManager;
        private SelectObjectForm objectForm;
        #endregion

        public MainForm()
        {
            InitializeComponent();

            mouseWatcher = eventHookFactory.GetMouseWatcher();
            keyboardWatcher = eventHookFactory.GetKeyboardWatcher();
            clipboardWatcher = eventHookFactory.GetClipboardWatcher();
            applicationWatcher = eventHookFactory.GetApplicationWatcher();
            // printWatcher = eventHookFactory.GetPrintWatcher();

            Application.ApplicationExit += OnApplicationExit;
            mouseWatcher.OnMouseInput += OnMouseInput;
            keyboardWatcher.OnKeyInput += OnKeyInput;
            clipboardWatcher.OnClipboardModified += OnClipboardModified;
            applicationWatcher.OnApplicationWindowChange += OnApplicationWindowChange;
            /* printWatcher.OnPrintEvent += OnPrintEvent;
            }; */
            screenshotMaker = new ScreenshotMaker();
            runner = new Runner();

            config = new Config();
            jobManager = new JobManager();
            objectManager = new ObjectManager();
            mousePoint = Mouse.GetScreenCenterPoint();
        }

        #region Events
        protected void MainForm_Load(object sender, EventArgs e)
        {
            startWatch();
            SetDefaultJob();
        }

        protected void OnApplicationExit(object sender, EventArgs e)
        {
            stopWatch();
            eventHookFactory.Dispose();
        }

        protected void btnLoadJob_Click(object sender, EventArgs e)
        {
            if (jobForm == null)
            {
                jobForm = new LoadJobForm(jobManager);
            }
            Mouse.MousePoint formLocation = Mouse.GetAppropriatedFormPoint(mousePoint, jobForm.Size.Width, jobForm.Size.Height);
            jobForm.Location = new System.Drawing.Point(formLocation.X, formLocation.Y);
            jobForm.ShowDialog(this);
            if (jobForm.DialogResult == DialogResult.OK)
            {
                if (jobForm.SelectedJob != null)
                {
                    job = jobForm.SelectedJob;
                    loadJob();
                    txtJobName.Text = job.ObjectName;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            job.Save();
        }

        protected void btnRun_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            runner.Run(job);
            // WindowState = FormWindowState.Normal;
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            // this.WindowState = FormWindowState.Minimized;
            BitmapSource bt = screenshotMaker.BeginSelectionImageFromScreen();
            if (bt == null) 
            {
                return;
            }
            string objName = Microsoft.VisualBasic.Interaction.InputBox("Enter object name", "Object identification", objectManager.getUniqueObjName());
            string imageSrc = $"{config.ImgDir}\\{objName}.bmp";
            ImageUtils.SaveImageToFile(bt, imageSrc);

            System.Windows.Point mousePoint = screenshotMaker.MouseClickRelativePoint;
            ActObject newActObj = new ActObject { Name = objName, ImageSrc = imageSrc };
            AddOperation(newActObj, (int)mousePoint.X, (int)mousePoint.Y);
            // this.WindowState = FormWindowState.Normal;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (objectForm == null)
            {
                objectForm = new SelectObjectForm();
            }
            else
            {
                objectForm.FillObjects();
            }
            Mouse.MousePoint formLocation = Mouse.GetAppropriatedFormPoint(mousePoint, objectForm.Size.Width, objectForm.Size.Height);
            objectForm.Location = new System.Drawing.Point(formLocation.X, formLocation.Y);
            objectForm.ShowDialog(this);
            if (objectForm.DialogResult == DialogResult.OK)
            {
                AddOperation(objectForm.SelectedObject);
            }
        }
        #endregion

        #region Private methods
        private void AddOperation(ActObject obj) 
        {
            System.Windows.Point screenShotPoint = screenshotMaker.GetCenterPoint();
            AddOperation(obj, (int)screenShotPoint.X, (int)screenShotPoint.Y);
        }

        private void AddOperation(ActObject obj, int x, int y)
        {
            objectManager.Add(obj);
            job.Add(obj, x, y);
        }

        /* Не працює в режимі відладки */
        private void startWatch()
        {
            // mouseWatcher.Start();
            keyboardWatcher.Start();
            /* clipboardWatcher.Start();
            applicationWatcher.Start(); */
            // printWatcher.Start();
        }

        private void stopWatch()
        {
            keyboardWatcher.Stop();
            // mouseWatcher.Stop();
            /* clipboardWatcher.Stop();
            applicationWatcher.Stop(); */
            // printWatcher.Stop();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnMouseInput(object sender, EventHook.MouseEventArgs e)
        {
            mousePoint.X = e.Point.x;
            mousePoint.Y = e.Point.y;
        }

        private void OnKeyInput(object sender, KeyInputEventArgs e)
        {
            
        }

        private void OnClipboardModified(object sender, ClipboardEventArgs e)
        {
            // Console.WriteLine("Clipboard updated with data '{0}' of format {1}", e.Data, e.DataFormat.ToString());
        }

        private void OnApplicationWindowChange(object sender, ApplicationEventArgs e)
        {
            // Console.WriteLine("Application window of '{0}' with the title '{1}' was {2}", e.ApplicationData.AppName, e.ApplicationData.AppTitle, e.Event);
        }

        private void OnPrintEvent(object sender, PrintEventArgs e)
        {
            // Console.WriteLine("Printer '{0}' currently printing {1} pages.", e.EventData.PrinterName, e.EventData.Pages);
        }

        private void SetDefaultJob() 
        {
            string jobName = jobManager.GetNewJobName();
            job = new Job(jobName, config.DataDir);
            txtJobName.Text = jobName;
        }

        private void grdOperations_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /* if (job == null)
            {
                return;
            }
            DataRow row = tblOperations.Rows[e.RowIndex];
            int id = (int)row["Id"];
            switch (grdOperations.Columns[e.ColumnIndex].Name)
            {
                case "clDel":
                    if (MessageBox.Show("Are you sure want to delete this record?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) 
                    {
                        grdOperations.Rows.RemoveAt(e.RowIndex);
                        // row.Delete();
                        job.Delete<Operation>(id);
                    }
                    break;
                case "clImage":
                    Operation op = job.GetItem<Operation>(id);
                    if (op.Actor != null && op.Actor.ImageSrc.Length > 0)
                    {
                        ImageView imageView = new ImageView();
                        imageView.LoadImage(op.Actor.ImageSrc);
                        Mouse.MousePoint formLocation = Mouse.GetAppropriatedFormPoint(mousePoint, imageView.Size.Width, imageView.Size.Height);
                        imageView.Location = new System.Drawing.Point(formLocation.X, formLocation.Y);
                        imageView.Show();
                    }
                    break;
            } */
        }

        private void loadJob() 
        {
            job.Load();
            pnlOperations.Controls.Clear();
            pnlOperations.Controls.AddRange(job.Panels.ToArray());
        }
        #endregion
    }
}
