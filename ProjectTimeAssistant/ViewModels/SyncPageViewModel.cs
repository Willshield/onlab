using ProjectTimeAssistant.Models;
using ProjectTimeAssistant.Services.DataService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace ProjectTimeAssistant.ViewModels
{
    public class SyncPageViewModel : ViewModelBase
    {

        IDataService DataService;
        private ObservableCollection<WorkTime> list;
        public ObservableCollection<WorkTime> List
        {
            get { return list; }
            set
            {
                Set(ref list, value);
            }
        }

        public SyncPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                DataService = new DesignTimeDataService();
                Refresh();
            }
            else
            {
                DataService = DataSource.Instance;
                Refresh();
            }
        }

        public void Refresh()
        {
            List = DataService.GetDirtyWorkTimes();
        }

    }
}
