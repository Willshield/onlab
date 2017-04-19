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
    public class WorkTimePageViewModel : ViewModelBase
    {
        private ObservableCollection<WorkTime> list;

        public ObservableCollection<WorkTime> List
        {
            get { return list; }
            set { Set(ref list, value); }
        }

        public WorkTimePageViewModel()
        {
            RefreshCommand = new DelegateCommand(Refresh);
            OrderCommand = new DelegateCommand(Order);

            //init first and only datasource, pull
            DataSource ds = DataSource.Instance;
            List = new ObservableCollection<WorkTime>();

            Refresh();
        }

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
                    List = new ObservableCollection<WorkTime>(List.OrderBy(i => i.Issue.Tracker));
                    break;
                case 1:
                    List = new ObservableCollection<WorkTime>(List.OrderBy(i => i.Issue.Project.Name));
                    break;
                case 2:
                    List = new ObservableCollection<WorkTime>(List.OrderBy(i => i.Issue.Subject));
                    break;
                case 3:
                    List = new ObservableCollection<WorkTime>(List.OrderBy(i => i.StartTime));
                    break;
                case 4:
                    List = new ObservableCollection<WorkTime>(List.OrderBy(i => i.Hours));
                    break;
            }
            
        }
        public void Refresh()
        {
            List = new ObservableCollection<WorkTime>(DataSource.Instance.getWorkTimes());
        }
    }
}
