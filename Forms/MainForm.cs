using System;
using System.Collections.Generic;
using System.Data;
using System.Resources;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Threading;
using System.Threading.Tasks;
using AutomatedWorker.Base;
using AutomatedWorker.Data;
using AutomatedWorker.Properties;
using EventHook;
using EventHook.Tools;

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

        // private readonly PrintWatcher printWatcher;
        private Config config;
        private Job job;
        private JobManager jobManager;
        private LoadJobForm jobForm;
        private ObjectManager objectManager;
        private SelectObjectForm objectForm;

        private ResourceManager resManager;
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

            config = new Config();
            jobManager = new JobManager();
            objectManager = new ObjectManager();
            mousePoint = Mouse.GetScreenCenterPoint();

            resManager = new ResourceManager("AutomatedWorker.Properties.Resources", typeof(Resources).Assembly);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            startWatch();
            FillLookups();
            SetDefaultJob();
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            stopWatch();
            eventHookFactory.Dispose();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            // this.WindowState = FormWindowState.Minimized;
            BitmapSource bt = ScreenshotMaker.BeginSelectionImageFromScreen();
            if (bt == null) 
            {
                return;
            }
            string objName = Microsoft.VisualBasic.Interaction.InputBox("Enter object name", "Object identification", objectManager.getUniqueObjName());
            string imageSrc = $"{config.ImgDir}\\{objName}.bmp";
            ImageUtils.SaveImageToFile(bt, imageSrc);

            System.Windows.Point mousePoint = ScreenshotMaker.MouseClickRelativePoint;
            ActObject newActObj = new ActObject { Name = objName, ImageSrc = imageSrc };
            AddOperation(newActObj, (int)mousePoint.X, (int)mousePoint.Y);
            // this.WindowState = FormWindowState.Normal;
        }

        private void btnAdd_Click(object sender, EventArgs e)
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

        private void AddOperation(ActObject obj) 
        {
            System.Windows.Point screenShotPoint = ScreenshotMaker.GetCenterPoint();
            AddOperation(obj, (int)screenShotPoint.X, (int)screenShotPoint.Y);
        }

        private void AddOperation(ActObject obj, int x, int y) 
        {
            objectManager.Add(obj);

            Act newAct = new Act { ActPoint = new Mouse.MousePoint { X = x, Y = y }, ClickType = Config.DEFMOUSE_CLICKTYPE };
            int newActId = job.GetCount<Operation>() + 1;
            Operation newOp = new Operation { Id = newActId, Actor = obj, Action = newAct };
            job.Add<Operation>(newOp);

            Byte[] imgData = ImageUtils.LoadImageFromFile(obj.ImageSrc);
            addRow(newOp, imgData);
        }

        private void addRow(Operation op, Byte[] imgData)
        {
            tblOperations.Rows.Add(op.Id, op.Name, op.Action.ActPoint.X, op.Action.ActPoint.Y, null, imgData, (int)op.Action.ClickType);
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
            if (job == null)
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
            }
        }

        private void btnLoadJob_Click(object sender, EventArgs e)
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

        private void loadJob() 
        {
            tblOperations.Rows.Clear();
            foreach (Operation op in job.GetItems<Operation>())
            {
                System.Drawing.Bitmap img = new System.Drawing.Bitmap(op.Actor.ImageSrc);
                Byte[] imgData = ImageUtils.BitmapToByteArray(img);
                tblOperations.Rows.Add(op.Id, op.Name, op.Action.ActPoint.X, op.Action.ActPoint.Y, op.Action.KeyboardText, imgData, (int)op.Action.ClickType);
            }
        }

        private void grdOperations_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            /*if (job == null)
            {
                return;
            }
            int id = e.RowIndex + 1;
            Operation op = job.GetItem<Operation>(id);
            DataGridViewColumn viewCol = grdOperations.Columns[e.ColumnIndex];
            DataRow row = tblOperations.Rows[e.RowIndex];
            switch (viewCol.Name) 
            {
                case "clId":
                    op.Id = (int)row["Id"];
                    break;
                case "clName":
                    op.Name = (row["Name"] != null) ? (string)row["Name"] : null;
                    break;
                case "clMouseX":
                case "clMouseY":
                    op.Action.ActPoint = new Mouse.MousePoint { X = (int)row["MouseX"], Y = (int)row["MouseY"] };
                    break;
                case "clKeyboardText":
                    op.Action.KeyboardText = (row["KeyboardText"] != null) ? (string)row["KeyboardText"] : null;
                    break;
                case "clClickTypeId":
                    op.Action.ClickType = (MouseClickType)((int)row["ClickTypeId"]);
                    break;
            }
            job.Edit<Operation>(op); */
        }

        private void FillLookups() 
        {
            tblMouseClickType.Rows.Add(new object[] { 1, "LeftClick", "Left click" });
            tblMouseClickType.Rows.Add(new object[] { 2, "RightClick", "Right click" });
            tblMouseClickType.Rows.Add(new object[] { 3, "DoubleClick", "Double click" });
        }

        private void tblOperations_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            DataRow row = e.Row;
            int id = (int)row["Id"];
            Operation op = job.GetItem<Operation>(id);
            op.Name = (row["Name"] != DBNull.Value) ? (string)row["Name"] : "-";
            op.Action.ActPoint = new Mouse.MousePoint { X = (int)row["MouseX"], Y = (int)row["MouseY"] };
            op.Action.KeyboardText = (row["KeyboardText"] != DBNull.Value) ? (string)row["KeyboardText"] : null;
            op.Action.ClickType = (MouseClickType)(row["ClickTypeId"] != DBNull.Value ? (int)row["ClickTypeId"] : (int)Config.DEFMOUSE_CLICKTYPE);
            job.Edit<Operation>(op);
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            List<Operation> ops = job.GetItems<Operation>();
            if (ops.Count == 0) 
            {
                return;
            }
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            Task currentTask = Task.Factory.StartNew(() => runOperation(ops[0], source, ops.Count == 1), token,
                TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
            for (int i = 1; i < ops.Count; i++)
            {
                Operation op = ops[i];
                bool isLast = i == ops.Count - 1;
                currentTask = currentTask.ContinueWith(t => runOperation(op, source, isLast), token,
                    TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        private void runOperation(Operation op, CancellationTokenSource source, bool isLast)
        {
            CancellationToken token = source.Token;
            if (token.IsCancellationRequested)
            {
                return;
            }
            // Thread.Sleep(Mouse.WAIT_FOR_CLICK);
            System.Drawing.Bitmap img = new System.Drawing.Bitmap(op.Actor.ImageSrc);
            System.Drawing.Point? imgPoint = WorkerScreen.GetFragmentPoint(img);
            if (isLast)
            {
                Mouse.MoveTo(1620, 400);
            }
            else 
            {
            if (!imgPoint.HasValue)
            {
                MessageBox.Show(String.Format(resManager.GetString("ErrFragmentIsNotFound"), op.Name), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                source.Cancel();
                return;
            }
                Mouse.MoveTo(imgPoint.Value.X + op.Action.ActPoint.X, imgPoint.Value.Y + op.Action.ActPoint.Y);
            }
            switch (op.Action.ClickType)
            {
                case MouseClickType.LEFTCLICK:
                    Mouse.Click_Left();
                    break;
                case MouseClickType.RIGHTCLICK:
                    Mouse.Click_Left();
                    break;
                case MouseClickType.DOUBLECLICK:
                    Mouse.Click_Left();
                    break;
            }
            if (isLast) 
            {
                source.Dispose();
            }
        }
    }
}
