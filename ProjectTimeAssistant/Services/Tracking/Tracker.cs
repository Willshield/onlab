using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using ProjectTimeAssistant.Models;
using ProjectTimeAssistant.Services.DataService;
using Windows.UI.Xaml;

namespace ProjectTimeAssistant.Services.Tracking
{
    class Tracker
    {

        DispatcherTimer stopWatch;
        private static Tracker instance = null;
        private IDataService dataService;
        private Issue trackedIssue;
        private WorkTime trackedTime;

        public delegate void ChangedEventHandler(TimeSpan t);
        public delegate void NewTrackingStartedEventHandler();
        public event ChangedEventHandler TimeChanged;
        public event NewTrackingStartedEventHandler NewTracking;

        private TimeSpan time;
        public TimeSpan Time
        {
            get { return time; }
            private set {
                time = value;
                TimeChanged?.Invoke(value);
            }
        }


        private Tracker()
        {
            trackedTime = new WorkTime();
            trackedIssue = new Issue();
            time = new TimeSpan(0, 0, 0);
            stopWatch = new DispatcherTimer();
            stopWatch.Tick += DispatcherTimer_Tick;
            stopWatch.Interval = new TimeSpan(0, 0, 0, 0, 5); //todo: (0, 0, 1)
            dataService = DataSource.Instance;
        }

        public static Tracker Instance
        {
            get
            {

                if (instance == null)
                {
                    instance = new Tracker();
                }

                return instance;
            }
        }

        private void DispatcherTimer_Tick(object sender, object e)
        {
            Time = time.Add(new TimeSpan(0, 0, 1));
        }

        private void SetIssue(Issue issue)
        {
            if (!stopWatch.IsEnabled)
            {
                trackedIssue = issue;
            } else
            {
                StopTracking();
                trackedIssue = issue;
            }
        }

        public Issue GetTrackedIssue()
        {
            return trackedIssue;
        }

        public void StartTracking(Issue issue, string comment)
        {
            SetIssue(issue);
            trackedTime = new WorkTime();
            trackedTime.StartTime = DateTime.Now;
            trackedTime.Comment = comment;
            trackedTime.IssueID = issue.IssueID;
            stopWatch.Start();
            NewTracking();
        }

        public bool TrackingOn
        {
            get { return stopWatch.IsEnabled; }
        }

        public string Comment
        {
            get {return trackedTime.Comment; }
            set { trackedTime.Comment = value; }
        }

        public DateTime? StartTime
        {
            get { return trackedTime.StartTime; }
        }

        public string Priority
        {
            get { return trackedIssue.Priority; }
        }

        public string IssueDescription
        {
            get { return trackedIssue.Description; }
        }

        public string IssueTracker 
        {
            get { return trackedIssue.Tracker; }
        }

        public string ProjectName
        {
            get { return trackedIssue.Project == null ? "" : trackedIssue.Project.Name; }
        }

        public string IssueSubject
        {
            get { return trackedIssue.Subject; }
        }

        public void StopTracking()
        {
            if (stopWatch.IsEnabled)
            {
                stopWatch.Stop();
                trackedTime.IssueID = trackedIssue.IssueID;
                trackedTime.FinishTime = DateTime.Now;
                //Todo: kerekítési beállítások
                trackedTime.Hours = ((double)time.Hours) + ((double)time.Minutes / 60.0) + ((double) time.Seconds / 3600.0);

                //Database connetc requires to search this item TODO: uncomment to save to database
                ////dataService.AddTimeEntry(trackedTime);

                time = new TimeSpan(0, 0, 0);
                trackedIssue = null;
                trackedTime = new WorkTime();
            }
        }

        public void AbortTracking()
        {
            if (stopWatch.IsEnabled)
            {
                stopWatch.Stop();
                time = new TimeSpan(0, 0, 0);
                trackedIssue = null;
                trackedTime = new WorkTime();
            }
        }

    }
}
