using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using ProjectTimeAssistant.Models;

namespace ProjectTimeAssistant.Services.Tracking
{
    class Tracker
    {
        Issue trackedIssue;
        Stopwatch stopWatch;
        Tracker(Issue issue)
        {
            trackedIssue = issue;
            stopWatch = new Stopwatch();
        }

        public void StartTracking()
        {
            if(!stopWatch.IsRunning)
                stopWatch.Start();
        }

        public TimeSpan StopTracking()
        {
            if (stopWatch.IsRunning)
            {
                stopWatch.Stop();
                WorkTime trackedTime = new WorkTime();
                trackedTime.IssueID = trackedIssue.IssueID;
                //Database connetc requires to search this item
                trackedTime.Issue = trackedIssue;
                trackedTime.FinishTime = DateTime.Now;
                trackedTime.Hours = ((double) stopWatch.Elapsed.Hours) + ((double) stopWatch.Elapsed.Minutes) / 60.0;
            }
            return stopWatch.Elapsed;    
        }
    }
}
