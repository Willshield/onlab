using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using ProjectTimeAssistant.Models;
using ProjectTimeAssistant.Services.DataService;
using Windows.UI.Xaml;
using Windows.UI.Popups;

namespace ProjectTimeAssistant.Services.Tracking
{
    class Tracker
    {

        DispatcherTimer stopWatch;
        private static Tracker instance = null;
        private IDataService dataService;

        private Issue trackedIssue;
        private Issue TrackedIssue {
            get { return trackedIssue; }
            set
            {
                lastTracked = trackedIssue;
                trackedIssue = value;
            }
        }

        private Issue lastTracked;
        public Issue LastTracked
        {
            get { return lastTracked; }
        }
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
            TrackedIssue = new Issue();
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

        private async Task<bool> SetIssue(Issue issue)
        {
            lastTracked = TrackedIssue;
            if (!stopWatch.IsEnabled)
            {
                TrackedIssue = issue;
                return true;
            } else
            {
                MessageDialog dialog = getAskDialog();
                var cmd = await dialog.ShowAsync();
                if (cmd.Label == "Cancel")
                {
                    return false;
                } else if (cmd.Label == "Yes")
                {
                    StopTracking();
                    TrackedIssue = issue;
                    return true;
                }
                else if (cmd.Label == "No")
                {
                    AbortTracking();
                    TrackedIssue = issue;
                    return true;
                }
                return false;
            }
        }

        public Issue GetTrackedIssue()
        {
            return TrackedIssue;
        }

        public MessageDialog getAskDialog()
        {
            MessageDialog dialog = new MessageDialog("You are already tracking an issue. Do you want to save its time? Select cancel if you want to stay tracking.", "Already tracking an issue");
            dialog.Commands.Add(new UICommand("Yes", null));
            dialog.Commands.Add(new UICommand("No", null));
            dialog.Commands.Add(new UICommand("Cancel", null));
            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 2;
            return dialog;
        }

        public async void AskStartTracking(Issue issue, string comment)
        {
            bool cond = await SetIssue(issue);
            if (cond)
            {
                trackedTime = new WorkTime();
                trackedTime.StartTime = DateTime.Now;
                trackedTime.Comment = comment;
                trackedTime.IssueID = issue.IssueID;
                stopWatch.Start();
                NewTracking();
            }
            
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
            get { return TrackedIssue.Priority; }
        }

        public string IssueDescription
        {
            get { return TrackedIssue.Description; }
        }

        public string IssueTracker 
        {
            get { return TrackedIssue.Tracker; }
        }

        public string ProjectName
        {
            get { return TrackedIssue.Project == null ? "" : TrackedIssue.Project.Name; }
        }

        public string IssueSubject
        {
            get { return TrackedIssue.Subject;  }
        }

        public void StopTracking()
        {
            if (stopWatch.IsEnabled)
            {
                stopWatch.Stop();
                trackedTime.IssueID = TrackedIssue.IssueID;
                trackedTime.FinishTime = DateTime.Now;

                //Todo: kerekítési beállítások
                trackedTime.Hours = ((double)time.Hours) + ((double)time.Minutes / 60.0) + ((double) time.Seconds / 3600.0);

                //Database connetc requires to search this item
                dataService.AddTimeEntry(trackedTime);

                time = new TimeSpan(0, 0, 0);
                TrackedIssue = new Issue();
                trackedTime = new WorkTime();
                NewTracking();
            }
        }

        public void AbortTracking()
        {
            if (stopWatch.IsEnabled)
            {
                stopWatch.Stop();
                time = new TimeSpan(0, 0, 0);
                TrackedIssue = new Issue();
                trackedTime = new WorkTime();
                NewTracking();
            }
        }

    }
}
