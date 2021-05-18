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
            PopulateJobCombo();
        }

        private void PopulateJobCombo() 
        {
            if (jobManager.JobNames.Count > 0)
            {
                foreach (string jobName in jobManager.JobNames)
                {
                    cmbJob.Items.Add(jobName);
                }
                cmbJob.SelectedIndex = 0;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            selectedJobName = cmbJob.Text;
        }
    }
}
