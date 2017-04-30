using ProjectTimeAssistant.Services.DataService;
using ProjectTimeAssistant.Services.SettingsServices;
using ProjectTimeAssistant.Services.Tracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Popups;

namespace ProjectTimeAssistant.ViewModels
{
    public class ActuallyTrackingViewModel : ViewModelBase
    {
        private Tracker tracker;

        public ActuallyTrackingViewModel()
        {
            AbortCommand = new DelegateCommand(AbortTracking);
            StopSaveCommand = new DelegateCommand(StopSaveTracking);

            tracker = Tracker.Instance;
            timeStamp = "00:00:00";
            tracker.TimeChanged += TimeChangedEventHandler;
            tracker.NewTracking += RefreshDisplayedData;
        }

        public void RefreshDisplayedData()
        {
            Comment = "";
            TimeStamp = "00:00:00";

        }

        private void TimeChangedEventHandler(TimeSpan t)
        {
            TimeStamp = t.ToString(); 
        }

        private string timeStamp;
        public string TimeStamp
        {
            get { return timeStamp; }
            set { Set(ref timeStamp, value); }
        }

        private string comment;
        public string Comment
        {
            get { return tracker.Comment; }
            set
            {
                Set(ref comment, value);
                tracker.Comment = value;
            }
        }

        public DateTime? StartTime
        {
            get { return tracker.StartTime; }
        }

        public string Priority
        {
            get { return tracker.Priority; }
        }

        public double AllWorkingTime
        {
            get
            {
                if(tracker.GetTrackedIssue() != null)
                {
                    return DataSource.Instance.getAllWorkingTime(tracker.GetTrackedIssue());
                } else { return 0; }   
            }          
        }

        public DelegateCommand AbortCommand { get; }
        public void AbortTracking()
        {
            tracker.AskAbortTracking();
        }

        public DelegateCommand StopSaveCommand { get; }
        public  void StopSaveTracking()
        {
            tracker.AskStopTracking();
        }

        public string Description
        {
            get { return tracker.IssueDescription; }
        }

        public string IssueTracker
        {
            get { return tracker.IssueTracker; }
        }

        public string ProjectName
        {
            get { return tracker.ProjectName; }
        }

        public string Subject
        {
            get { return tracker.IssueSubject; }
        }


    }
}
