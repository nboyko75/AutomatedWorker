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
