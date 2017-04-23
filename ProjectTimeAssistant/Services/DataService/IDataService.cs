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
        ObservableCollection<Issue> getIssues();
        ObservableCollection<WorkTime> getWorkTimes();

    }

}
