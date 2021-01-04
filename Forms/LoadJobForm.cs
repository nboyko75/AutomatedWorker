using System;
using System.Windows.Forms;
using AutomatedWorker.Data;
using AutomatedWorker.Base;

namespace AutomatedWorker.Forms
{
    public partial class LoadJobForm : Form
    {
        public string SelectedJobName { get { return selectedJobName; } }
        public Job SelectedJob { get { return selectedJob; } }

        private JobManager jobManager;
        private string selectedJobName;
        private Job selectedJob;

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
            if (jobManager.Jobs.Count > 0) 
            {
                foreach (Job j in jobManager.Jobs)
                {
                    cmbJob.Items.Add(j.ObjectName);
                }
                cmbJob.SelectedIndex = 0;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            selectedJobName = cmbJob.Text;
            selectedJob = jobManager.Jobs.Find(j => (j.ObjectName == selectedJobName));
        }
    }
}
