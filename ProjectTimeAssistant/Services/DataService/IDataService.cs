using ProjectTimeAssistant.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTimeAssistant.Services.DataService
{
    interface IDataService
    {
        ObservableCollection<Issue> GetIssues();
        ObservableCollection<WorkTime> GetWorkTimes();
        ObservableCollection<Issue> GetFavouriteIssues();
        Issue GetActuallyTracked();
        void AddTimeEntry(WorkTime workTime);
        double GetAllWorkingTime(Issue issue);
        Issue GetIssueById(int id);
        void SetFavourite(int id, bool isFavourite);
    }

}
