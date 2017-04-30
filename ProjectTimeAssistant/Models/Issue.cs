using ProjectTimeAssistant.Services.DataService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Template10.Mvvm;

namespace ProjectTimeAssistant.Models
{
    public class Issue
    {
        public int IssueID { get; set; }

        private string tracker;
        public string Tracker
        {
            get { return tracker; }
            set { tracker = value; }
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

        public bool IsFavourite { get; set; }

        public string Priority
        {
            get;
            set;
        }

        public DateTime Updated
        {
            get;
            set;
        }

        //Navigation
        public int ProjectID { get; set; }
        public Project Project { get; set; }
        public List<WorkTime> WorkTimes { get; set; }

        //Methods, and properties:
        [NotMapped] 
        public double AllTrackedTime
        {
            get
            {
                return DataSource.Instance.getAllWorkingTime(this);
            }
        }

        [NotMapped]
        public Template10.Mvvm.AwaitableDelegateCommand TrackingCommand { get { return new Template10.Mvvm.AwaitableDelegateCommand(StartTracking); } }
        public async Task StartTracking(AwaitableDelegateCommandParameter arg)
        {
            await Services.Tracking.Tracker.Instance.AskStartTracking(this, "");
        }


    }
}
