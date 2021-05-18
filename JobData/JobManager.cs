using System;
using System.Collections.Generic;
using System.IO;

namespace JobData
{
    public class JobManager
    {
        private Config config;
        private List<string> jobNames;

        public List<string> JobNames { get { return jobNames; } }

        public JobManager() 
        {
            config = new Config();
            jobNames = new List<string>();
            FillJobs();
        }

        public Job GetJob(string jobName)
        {
            return new Job(jobName, config.DataDir);
        }

        private void FillJobs()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(config.DataDir);
            foreach (FileInfo file in dirInfo.GetFiles("*.json", SearchOption.AllDirectories))
            {
                jobNames.Add(Path.GetFileNameWithoutExtension(file.Name));
            }
            jobNames.Sort((j1, j2) => j1.CompareTo(j2));
        }
    }
}
