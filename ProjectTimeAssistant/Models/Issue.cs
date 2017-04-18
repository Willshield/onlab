using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTimeAssistant.Models
{
    class Issue
    {
        private int id;
        public int ID
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

        private string project;
        public string Project
        {
            get { return project; }
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

        //kapcsolat
        private List<WorkTime> worklist;
        public Issue()
        {
            //TODO: Init params

            //worklist = new List<WorkTime>();
        }

        public Issue(int id, string tracker, string project, string subject, string description)
        {
            this.id = id;
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

        public string Priority
        {
            get;
            //TODO: validation
            set;
        }

        //TODO:
        public DateTime Updated
        {
            get;
            //TODO: validation
            set;
        }

        //public void addWorktime(WorkTime wt)
        //{
        //    worklist.Add(wt);
        //}

        //public void clearWorktime()
        //{
        //    worklist.Clear();
        //}

        //TODO: sum worktime
    }
}
