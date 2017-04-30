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
        public void AddTimeEntry(WorkTime workTime)
        {
            throw new NotImplementedException();
        }

        public Issue GetActuallyTracked()
        {
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
            return tmp;
        }

        public double GetAllWorkingTime(Issue issue)
        {
            return 10.4;
        }

        public Issue GetIssueById(int id)
        {
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
            return tmp;
        }

        public ObservableCollection<Issue> GetIssues()
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

        public ObservableCollection<WorkTime> GetWorkTimes()
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

        public void SetFavourite(int id, bool isFavourite)
        {
            throw new NotImplementedException();
        }
    }
}
