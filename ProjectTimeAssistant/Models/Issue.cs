using ProjectTimeAssistant.Services.DataService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTimeAssistant.Models
{
    public class Issue
    {
        public int IssueID { get; set; }

        private string tracker;
        public string Tracker
        {
            get { return tracker; }
            //TODO: validators
            set { tracker = value; }
        }

        //private string project;
        //public string Project
        //{
        //    get { return project; }
        //    set { project = value; }
        //}

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
        public List<WorkTime> WorkTimes { get; set; }
        //public Issue()
        //{
        //    //TODO: Init params

        //    //worklist = new List<WorkTime>();
        //}

        //public Issue(int id, string tracker, string project, string subject, string description)
        //{
        //    this.IssueID = id;
        //    this.Tracker = tracker;
        //    this.Project = project;
        //    this.Subject = subject;
        //    this.Description = description;

        //    //worklist = new List<WorkTime>();
        //}

        ////TODO:
        //public string AllWorkingTime
        //{
        //    get { return "No tracked time"; }
        //}

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

        //Navigation
        public int ProjectID { get; set; }
        public Project Project { get; set; }


        //Methods:
        [NotMapped]
        public double AllTrackedTime
        {
            get
            {
                var t = DataSource.Instance.getAllWorkTimes(this.IssueID);
                return 0;
            }
        }
        
    }
}
