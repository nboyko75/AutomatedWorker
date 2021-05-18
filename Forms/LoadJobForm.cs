using System;
using System.Windows.Forms;
using JobData;

namespace AutomatedWorker.Forms
{
    public partial class LoadJobForm : Form
    {
        public string SelectedJobName { get { return selectedJobName; } }

        private JobManager jobManager;
        private string selectedJobName;

        public LoadJobForm() : this (new JobManager())
        {
        }

        public LoadJobForm(JobManager jobMng)
        {
            InitializeComponent();
            jobManager = jobMng;
            PopulateJobs();
        }

        protected void lstJobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = true;
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

        private void PopulateJobs() 
        {
            if (jobManager.JobNames.Count > 0)
            {
                foreach (string jobName in jobManager.JobNames)
                {
                    lstJobs.Items.Add(jobName);
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            selectedJobName = lstJobs.SelectedItem.ToString();
        }
    }
}
