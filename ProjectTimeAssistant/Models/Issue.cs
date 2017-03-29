using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTimeAssistant.Model
{
    class Issue
    {
        private Guid id;
        public Guid ID
        {
            get { return id; }
        }

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

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private bool dirty;
        public bool Dirty
        {
            get { return dirty; }
            set { dirty = value; }
        }

        private List<WorkTime> worklist;
        public Issue()
        {
            //id = new Guid("dddd-dddd");
            id = Guid.NewGuid();

            //TODO: Init params

            worklist = new List<WorkTime>();
        }
        public Issue(string tracker, string project, string subject, string description)
        {
            id = new Guid("dddd-dddd");
            id = Guid.NewGuid();

            this.Tracker = tracker;
            this.Project = project;
            this.Subject = subject;
            this.Description = description;

            //worklist = new List<WorkTime>();
        }

        //TODO:
        public string AllWorkingTime
        {
            get { return "No tracked time"; }
        }

        //TODO:
        public string Priority
        {
            get
            {
                Random r = new Random();
                int i = r.Next();
                if (i % 3 == 0)
                {
                    return "Normal";
                }
                else if (i % 3 == 1)
                {
                    return "High";
                }
                else
                {
                    return "low";
                }

            }
        }

        //TODO:
        public string DueDate
        {
            get { return DateTime.Now.ToString(); }
        }

        public void addWorktime(WorkTime wt)
        {
            worklist.Add(wt);
        }

        public void clearWorktime()
        {
            worklist.Clear();
        }

        //TODO: sum worktime
    }
}
