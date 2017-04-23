using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTimeAssistant.Models;

namespace ProjectTimeAssistant.Services.DataService
{
    class DesignTimeDataService : IDataService
    {
        public ObservableCollection<Issue> getIssues()
        {
            ObservableCollection<Models.Issue> list = new ObservableCollection<Models.Issue>();

            Models.Issue tmp = new Models.Issue();
            tmp.IssueID = 1;
            tmp.Tracker = "Tracker";
            tmp.Project = new Models.Project();
            tmp.ProjectID = 1;
            tmp.Project.Name = "ProjectName";
            tmp.Subject = "Subject";
            tmp.Description = "Description";
            tmp.Priority = "Priority";
            tmp.Updated = DateTime.Now;

            list.Add(tmp);

            return list;

        }

        public ObservableCollection<WorkTime> getWorkTimes()
        {
            ObservableCollection<Models.WorkTime> list = new ObservableCollection<Models.WorkTime>();

            Models.WorkTime tmp = new WorkTime();
            tmp.WorkTimeID = 1;
            tmp.Hours = 1.0;
            tmp.StartTime = DateTime.Now;
            tmp.IssueID = 1;
            tmp.Comment = "Comment";

            list.Add(tmp);

            return list;
        }
    }
}
