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
        

        public string Priority
        {
            get;
            set;
        }

        //TODO:
        public DateTime Updated
        {
            get;
            set;
        }

        //Navigation
        public int ProjectID { get; set; }
        public Project Project { get; set; }


        //Methods:
        [NotMapped] //cant reach.............
        public double AllTrackedTime
        {
            get
            {
                var t = WorkTimes.Where(wt => wt.IssueID == this.IssueID).Sum(wt => wt.Hours);
                return t;
            }
        }

    }
}
