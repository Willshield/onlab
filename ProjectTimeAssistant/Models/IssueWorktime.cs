using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTimeAssistant.Models
{
     public class IssueWorktime
    {

        private string tracker;
        public string Tracker
        {
            get { return tracker; }
            //TODO: validators
            set { tracker = value; }
        }

        //TODO: query to projects
        private string project;
        public string Project
        {
            get { return project; }
            //TODO: desable this
            set { project = value; }
        }

        private string subject;
        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }

        private DateTime? startTime;
        public DateTime? StartTime
        {
            get { return startTime; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Starttime must be set");
                }
                else if (value > FinishTime)
                {
                    throw new ArgumentException("Work must be started before finished");
                }

                startTime = value;
            }
        }


        private DateTime? finishTime;
        public DateTime? FinishTime
        {
            get { return finishTime; }
            set
            {
                if (value != null)
                {
                    if (value < StartTime)
                    {
                        throw new ArgumentException("Work must be started before finished");
                    }
                }

                finishTime = value;
            }
        }

        public IssueWorktime() { }

        public IssueWorktime(string tracker, string project, string subject, DateTime? startTime, DateTime? finishTime )
        {

            this.Tracker = tracker;
            this.Project = project;
            this.Subject = subject;
            this.StartTime = startTime;
            this.FinishTime = finishTime;
            
        }

    }
}
