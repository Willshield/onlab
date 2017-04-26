using Microsoft.EntityFrameworkCore;
using ProjectTimeAssistant.Models;
using ProjectTimeAssistant.Services.DataBase;
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
    public class WorkTimePageViewModel : ViewModelBase
    {
        private ObservableCollection<WorkTime> list;

        public ObservableCollection<WorkTime> List
        {
            get { return list; }
            set { Set(ref list, value); }
        }


        IDataService DataService;

        public WorkTimePageViewModel()
        {
            RefreshCommand = new DelegateCommand(Refresh);
            OrderCommand = new DelegateCommand(Order);
            StartTrackingCommand = new DelegateCommand(StartTracking);
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                DataService = new DesignTimeDataService();
                Refresh();
            } else
            {
                DataService = DataSource.Instance;
                Refresh();
                SelectedItem = 4;
            }

        }

        public DelegateCommand StartTrackingCommand { get; }
        public void StartTracking()
        {
            if (SelectedWorkTime == null)
                return;
            using (var db = new DataContext())
            {
                Issue issue = db.Issues.Where(i => i.IssueID == SelectedWorkTime.IssueID).Include(i => i.Project).Single();
                Services.Tracking.Tracker.Instance.StartTracking(issue, "comment");
            }

        }
        public WorkTime SelectedWorkTime { get; set; }

        public readonly ObservableCollection<string> OrderingCats;
        private int selectedItem;
        public int SelectedItem { get { return selectedItem; } set { Set(ref selectedItem, value); Order(); } }
        public DelegateCommand RefreshCommand { get; }
        public DelegateCommand OrderCommand { get; }
        public void Order()
        {
            switch (SelectedItem)
            {
                case 0:
                    List = new ObservableCollection<WorkTime>(List.OrderBy(i => i.Issue.Subject));
                    break;
                case 1:
                    List = new ObservableCollection<WorkTime>(List.OrderBy(i => i.Issue.Project.Name));
                    break;
                case 2:
                    List = new ObservableCollection<WorkTime>(List.OrderBy(i => i.StartTime));
                    break;
                case 3:
                    List = new ObservableCollection<WorkTime>(List.OrderBy(i => i.Hours));
                    break;
                case 4:
                    List = new ObservableCollection<WorkTime>(List.OrderBy(i => i.Comment));
                    break;
            }
            
        }


        public void Refresh()
        {
            List = DataService.getWorkTimes();
        }
    }
}
