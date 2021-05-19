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
        private const string REGBASE = "Software";
        private const string REGAPP = "AutomatedWorker";
        private const int defaultMousePadding = 10;

        #region Attributes
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

            screenshotMaker = new ScreenshotMaker();
            runner = new Runner();
            config = new Config();
            jobManager = new JobManager();
            jobForm = new LoadJobForm(jobManager);
            objectManager = new ObjectManager();
            mousePoint = Mouse.GetScreenCenterPoint();
        }

        #region Events
        protected void MainForm_Load(object sender, EventArgs e)
        {
            string jobName = Properties.Settings.Default.LastJobName;
            txtJobName.Text = jobName;
            SetDefaultJob(jobName);

            System.Drawing.Point savedLocation = Properties.Settings.Default.MainForm_Location;
            if (savedLocation.X > 0 || savedLocation.Y > 0) 
            {
                Location = savedLocation;
            }
            System.Drawing.Size savedSize = Properties.Settings.Default.MainForm_Size;
            if (savedSize.Width > 0 || savedSize.Height > 0) 
            {
                Size = savedSize;
            }
        }

        protected void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string jobName = txtJobName.Text;
            if (!string.IsNullOrEmpty(jobName))
            {
                saveJob();
                Properties.Settings.Default.LastJobName = jobName;
            }
            Properties.Settings.Default.MainForm_Location = Location;
            Properties.Settings.Default.MainForm_Size = Size;
            Properties.Settings.Default.Save();
        }

        protected void btnLoadJob_Click(object sender, EventArgs e)
        {
            Mouse.MousePoint formLocation = Mouse.GetAppropriatedFormPoint(mousePoint, jobForm.Size.Width, jobForm.Size.Height);
            jobForm.Location = new System.Drawing.Point(formLocation.X, formLocation.Y);
            jobForm.ShowDialog(this);
            if (jobForm.DialogResult == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(jobForm.SelectedJobName))
                {
                    job = jobManager.GetJob(jobForm.SelectedJobName);
                    loadJob();
                    txtJobName.Text = job.ObjectName;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            saveJob();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            pnlOperations.Controls.Clear();
            job.Clear();
        }

        protected void btnRun_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            runner.Run(job);
            // WindowState = FormWindowState.Normal;
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            this.Hide();
            BitmapSource bt = screenshotMaker.BeginSelectionImageFromScreen();
            if (bt == null) 
            {
                return;
            }
            string objName = Microsoft.VisualBasic.Interaction.InputBox("Enter object name", "Object identification", objectManager.getUniqueObjName());
            string imageSrc = $"{config.ImgDir}\\{objName}.png";
            ImageUtils.SaveImageToFile(bt, imageSrc);

            System.Windows.Point mousePoint = screenshotMaker.MouseClickRelativePoint;
            ActObject newActObj = new ActObject { Name = objName, ImageSrc = imageSrc };
            AddOperation(newActObj, (int)mousePoint.X, (int)mousePoint.Y);
            this.Show();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (objectForm == null)
            {
                objectForm = new SelectObjectForm();
            }
            else
            {
                objectForm.PopulateObjects();
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

        private void SetDefaultJob(string jobName) 
        {
            job = new Job(jobName, config.DataDir);
            loadJob();
        }

        private void loadJob() 
        {
            job.setOwnerControl(pnlOperations);
            job.Load();
        }

        private void saveJob() 
        {
            string jobName = txtJobName.Text;
            if (job.ObjectName != jobName) 
            {
                job = new Job(jobName, config.DataDir);
            }
            job.Save();
            jobManager.AddJob(jobName);
            jobForm.PopulateJobs();
        }
        #endregion
    }
}
