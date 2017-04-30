﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using ProjectTimeAssistant.Models;
using ProjectTimeAssistant.Services.DataService;
using Windows.UI.Xaml;
using Windows.UI.Popups;
using ProjectTimeAssistant.Services.SettingsServices;

namespace ProjectTimeAssistant.Services.Tracking
{
    class Tracker
    {


        private static Tracker instance = null;
        public delegate void ChangedEventHandler(TimeSpan t);
        public delegate void NewTrackingStartedEventHandler();
        public event ChangedEventHandler TimeChanged;
        public event NewTrackingStartedEventHandler NewTracking;
        private IDataService dataService;
        private PopupService popupService;
        DispatcherTimer stopWatch;

        private WorkTime trackedTime;
        private Issue trackedIssue;
        private Issue TrackedIssue
        {
            get { return trackedIssue; }
            set
            {
                if (trackedIssue != null && trackedIssue.IssueID != -1) { lastTracked = trackedIssue; }
                trackedIssue = value;
            }
        }

        private Issue lastTracked;
        public Issue LastTracked
        {
            get { return lastTracked; }
        }

        private TimeSpan time;
        public TimeSpan Time
        {
            get { return time; }
            private set {
                time = value;
                TimeChanged?.Invoke(value);
            }
        }

        public string Comment
        {
            get { return trackedTime.Comment; }
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
            get { return TrackedIssue.Subject; }
        }

        public bool IsTracking { get { return stopWatch.IsEnabled; } }
        private bool paused = false;
        public bool Paused { get { return paused; } }

        private Tracker()
        {
            SetStartValues();
            stopWatch = new DispatcherTimer();
            stopWatch.Tick += DispatcherTimer_Tick;
            stopWatch.Interval = new TimeSpan(0, 0, 0, 0, 5); //todo: (0, 0, 1)
            dataService = DataSource.Instance;
            popupService = new PopupService();
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

        public async Task AskStartTracking(Issue issue, string comment)
        {
            bool cond = await TrySetIssue(issue);
            if (cond)
            {
                trackedTime = new WorkTime();
                trackedTime.StartTime = DateTime.Now;
                trackedTime.Comment = comment;
                trackedTime.IssueID = issue.IssueID;
                stopWatch.Start();
                paused = false;
                NewTracking();
            }
        }

        private void SetStartValues()
        {
            time = new TimeSpan(0, 0, 0);
            var issue = new Issue();
            issue.IssueID = -1;
            TrackedIssue = issue;
            trackedTime = new WorkTime();
        }


        private void DispatcherTimer_Tick(object sender, object e)
        {
            Time = time.Add(new TimeSpan(0, 0, 1));
        }

        private async Task<bool> TrySetIssue(Issue issue)
        {
            if (!stopWatch.IsEnabled && paused == false)
            {
                TrackedIssue = issue;
                return true;
            } else
            {
                MessageDialog dialog = popupService.GetDefaultAskDialog("You are already tracking an issue. Do you want to save its time? Select cancel if you want to stay tracking.",
                                                                        "Already tracking an issue", true);
                var cmd = await dialog.ShowAsync();
                if (cmd.Label == PopupService.CANCEL)
                {
                    return false;
                } else if (cmd.Label == PopupService.YES)
                {
                    StopSaveTracking();
                    TrackedIssue = issue;
                    return true;
                }
                else if (cmd.Label == PopupService.NO)
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

        public async Task AskStopTracking()
        {
            if (stopWatch.IsEnabled || paused == true)
            {
                if (SettingsService.Instance.AskIfStop)
                {
                    MessageDialog dialog = popupService.GetDefaultAskDialog("Are you sure?", "Stop working on issue and save", false);
                    var cmd = await dialog.ShowAsync();
                    if (cmd.Label == "Yes")
                    {
                        StopSaveTracking();
                    }
                }else { StopSaveTracking(); }
            } else
            {
                await popupService.GetDefaultNotification("There's no tracked issue right now.", "Nothing to stop and save").ShowAsync();
            }
        }

        private void StopSaveTracking()
        {
            stopWatch.Stop();
            paused = false;
            trackedTime.IssueID = TrackedIssue.IssueID;
            trackedTime.FinishTime = DateTime.Now;

            //Todo: kerekítési beállítások
            trackedTime.Hours = ((double)time.Hours) + ((double)time.Minutes / 60.0) + ((double)time.Seconds / 3600.0);

            //Database connetc requires to search this item
            dataService.AddTimeEntry(trackedTime);

            SetStartValues();
            NewTracking();
        }

        public async Task AskAbortTracking()
        {
            if (stopWatch.IsEnabled || paused == true)
            {
                if (SettingsService.Instance.AskIfStop)
                {
                    MessageDialog dialog = popupService.GetDefaultAskDialog("Are you sure? Aborting will reset all worktime.", "Abort Tracking", false);
                    var cmd = await dialog.ShowAsync();
                    if (cmd.Label == "Yes")
                    {
                        AbortTracking();
                    }
                }else { AbortTracking(); }
            }
            else
            {
                await popupService.GetDefaultNotification("There's no tracked issue right now.", "Nothing to abort").ShowAsync();
            }
        }

        private void AbortTracking()
        {
            stopWatch.Stop();
            paused = false;
            SetStartValues();
            NewTracking();
        }

        public void PauseTracking()
        {
            if (stopWatch.IsEnabled)
            {
                stopWatch.Stop();
                paused = true;
            }
        }

        public async Task RestartTracking()
        {
            if (!stopWatch.IsEnabled && paused == true)
            {
                stopWatch.Start();
                paused = false;
            } else if (!stopWatch.IsEnabled && paused == false)
            {
               await AskStartTracking(lastTracked, "");
            }
        }

    }
}
