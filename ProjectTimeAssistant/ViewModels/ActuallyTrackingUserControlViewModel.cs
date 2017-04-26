using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace ProjectTimeAssistant.ViewModels
{
    public class ActuallyTrackingUserControlViewModel : ViewModelBase
    {
        Services.Tracking.Tracker tracker;
        public ActuallyTrackingUserControlViewModel()
        {
            tracker = Services.Tracking.Tracker.Instance;
            timeStamp = "00:00:00";
            tracker.TimeChanged += TimeChangedEventHandler;
            tracker.NewTracking += SetDisplayedData;

        }

        public string subject = "No tracked issue";
        public string Subject
        {
            get { return subject; }
            private set { Set(ref subject, value); }
        }

        public string project;
        public string Project
        {
            get { return project; }
            private set { Set(ref project, value); }
        }

        public void SetDisplayedData()
        {
            Subject = tracker.IssueSubject;
            Project = tracker.ProjectName;
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

    }
}
