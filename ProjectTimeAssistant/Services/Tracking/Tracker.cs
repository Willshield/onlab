using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using ProjectTimeAssistant.Models;
using ProjectTimeAssistant.Services.DataService;

namespace ProjectTimeAssistant.Services.Tracking
{
    class Tracker
    {
        private Issue trackedIssue;
        private static Stopwatch stopWatch;
        private static Tracker instance = null;
        private IDataService dataService;
        private WorkTime trackedTime;

        private Tracker()
        {
            stopWatch = new Stopwatch();
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

        private void setIssue(Issue issue)
        {
            if (!stopWatch.IsRunning)
            {
                trackedIssue = issue;
            } else
            {
                StopTracking();
                trackedIssue = issue;
            }
        }

        public void StartTracking(Issue issue, string comment)
        {
            setIssue(issue);
            trackedTime = new WorkTime();
            trackedTime.StartTime = DateTime.Now;
            trackedTime.Comment = comment;
            stopWatch.Start();
        }

        //ToDo: add setComment

        public void StopTracking()
        {
            if (stopWatch.IsRunning)
            {
                stopWatch.Stop();
                trackedTime.IssueID = trackedIssue.IssueID;
                //Database connetc requires to search this item
                trackedTime.FinishTime = DateTime.Now;
                trackedTime.Hours = ((double) stopWatch.Elapsed.Hours) + ((double) stopWatch.Elapsed.Minutes) / 60.0;
                dataService.AddTimeEntry(trackedTime);

                trackedIssue = null;
            }
        }


    }
}
