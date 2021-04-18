using System;
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
        private const int defaultMousePadding = 10;

        #region Attributes
        private EventHookFactory eventHookFactory;
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

            Application.ApplicationExit += OnApplicationExit;
            eventHookFactory = new EventHookFactory();
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
            SetDefaultJob();
        }

        protected void OnApplicationExit(object sender, EventArgs e)
        {
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            pnlOperations.Controls.Clear();
            job.Clear();
            SetDefaultJob();
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
            if (objectForm.DialogResult == DialogResult.OK && objectForm.SelectedObject != null)
            {
                AddOperation(objectForm.SelectedObject);
            }
        }
        #endregion

        #region Private methods
        private void AddOperation(ActObject obj)
        {
            AddOperation(obj, defaultMousePadding, defaultMousePadding);
        }

        private void AddOperation(ActObject obj, int x, int y)
        {
            objectManager.Add(obj);
            job.Add(obj, x, y);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetDefaultJob()
        {
            string jobName = txtJobName.Text;
            if (String.IsNullOrEmpty(jobName)) 
            {
                jobName = jobManager.GetNewJobName();
                txtJobName.Text = jobName;
            }
            SetDefaultJob(jobName);
        }

        private void SetDefaultJob(string jobName) 
        {
            job = new Job(jobName, config.DataDir);
            job.setOwnerControl(pnlOperations);
        }

        private void loadJob() 
        {
            job.setOwnerControl(pnlOperations);
            job.Load();
        }
        #endregion
    }
}
