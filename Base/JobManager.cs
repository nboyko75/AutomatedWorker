using System;
using System.Collections.Generic;
using System.IO;
using AutomatedWorker.Data;

namespace AutomatedWorker.Base
{
    public class JobManager
    {
        public const string DEFAULT_JOBNAME = "job";

        private Config config;
        private List<Job> jobs;

        public List<Job> Jobs { get { return jobs; } }

        public JobManager() 
        {
            config = new Config();
            jobs = new List<Job>();
            FillJobs();
        }

        public string GetNewJobName() 
        {
            int defNum = 1;
            List<int> nums = new List<int>();
            foreach (Job j in jobs) 
            {
                if (j.ObjectName.StartsWith(DEFAULT_JOBNAME)) 
                {
                    int n;
                    if (Int32.TryParse(j.ObjectName.Substring(DEFAULT_JOBNAME.Length), out n)) 
                    {
                        int jobOpCount = j.GetCount<Operation>();
                        if (jobOpCount > 0) 
                        {
                            nums.Add(n);
                        }
                    }
                }
            }
            int numCount = nums.Count;
            if (numCount > 0)
            {
                nums.Sort();
                bool notFound = true;
                int idx = 0;
                while (notFound)
                {
                    if (idx > numCount - 1)
                    {
                        notFound = false;
                    }
                    else
                    {
                        int currNum = nums[idx];
                        if (currNum > defNum)
                        {
                            notFound = false;
                        }
                        else
                        {
                            if (currNum == defNum)
                            {
                                defNum++;
                            }
                            idx++;
                        }
                    }
                }
            }
            return $"{DEFAULT_JOBNAME}{defNum}";
        }

        private void FillJobs()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(config.DataDir);
            foreach (FileInfo file in dirInfo.GetFiles("*.json", SearchOption.AllDirectories))
            {
                Job j = new Job(Path.GetFileNameWithoutExtension(file.Name), file.Directory.FullName);
                jobs.Add(j);
            }
            jobs.Sort((j1, j2) => j1.ObjectName.CompareTo(j2.ObjectName));
        }
    }
}
