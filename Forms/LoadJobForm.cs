using System;
using System.Windows.Forms;
using JobData;

namespace AutomatedWorker.Forms
{
    public partial class LoadJobForm : Form
    {
        public string SelectedJobName { get { return lstJobs.SelectedItem.ToString(); } }

        private JobManager jobManager;

        public LoadJobForm() : this (new JobManager())
        {
        }

        public LoadJobForm(JobManager jobMng)
        {
            InitializeComponent();
            jobManager = jobMng;
            PopulateJobs();
        }

        public void PopulateJobs()
        {
            lstJobs.Items.Clear();
            if (jobManager.JobNames.Count > 0)
            {
                foreach (string jobName in jobManager.JobNames)
                {
                    lstJobs.Items.Add(jobName);
                }
            }
        }

        protected void lstJobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = true;
            btnDelete.Enabled = true;
        }

        protected void LoadJobForm_Load(object sender, EventArgs e)
        {
            System.Drawing.Point savedLocation = Properties.Settings.Default.LoadForm_Location;
            if (savedLocation.X > 0 || savedLocation.Y > 0)
            {
                Location = savedLocation;
            }
            System.Drawing.Size savedSize = Properties.Settings.Default.LoadForm_Size;
            if (savedSize.Width > 0 || savedSize.Height > 0)
            {
                Size = savedSize;
            }
        }

        protected void LoadJobForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.LoadForm_Location = Location;
            Properties.Settings.Default.LoadForm_Size = Size;
            Properties.Settings.Default.Save();
        }

        protected void lstJobs_DoubleClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
