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
        public async void AbortTracking()
        {
            if (tracker.TrackingOn)
            {
                if (SettingsService.Instance.AskIfStop)
                {
                    MessageDialog dialog = new MessageDialog("Are you sure? Aborting will reset all worktime.", "Abort Tracking");
                    dialog.Commands.Add(new UICommand("Yes", null));
                    dialog.Commands.Add(new UICommand("No", null));
                    dialog.DefaultCommandIndex = 0;
                    dialog.CancelCommandIndex = 1;
                    var cmd = await dialog.ShowAsync();
                    if (cmd.Label == "Yes")
                    {
                        tracker.AbortTracking();
                    }
                }
                else
                {
                    tracker.AbortTracking();
                }
                //todo: nullázd ki a kiírtakat abort esetén
            }
            else
            {
                await new MessageDialog("There's no tracked issue right now.", "Nothing to abort").ShowAsync();
            }
        }

        public DelegateCommand StopSaveCommand { get; }
        public async void StopSaveTracking()
        {
            if (tracker.TrackingOn)
            {
                if (SettingsService.Instance.AskIfStop)
                {
                    MessageDialog dialog = new MessageDialog("Are you sure?", "Stop working on issue and save");
                    dialog.Commands.Add(new UICommand("Yes", null));
                    dialog.Commands.Add(new UICommand("No", null));
                    dialog.DefaultCommandIndex = 0;
                    dialog.CancelCommandIndex = 1;
                    var cmd = await dialog.ShowAsync();
                    if (cmd.Label == "Yes")
                    {
                        tracker.StopTracking();
                    }
                }
                    tracker.StopTracking();
            }
            else
            {
                await new MessageDialog("There's no tracked issue right now.", "Nothing to stop and save").ShowAsync();
            }
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
