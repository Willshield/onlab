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
        private ObservableCollection<IssueWorktime> list;

        public ObservableCollection<IssueWorktime> List
        {
            get { return list; }
            set { Set(ref list, value); }
        }

        public WorkTimePageViewModel()
        {
            RefreshCommand = new DelegateCommand(Refresh);
            OrderCommand = new DelegateCommand(Order);
            OrderingCats = new ObservableCollection<string>
            {
                "Tracker", "Project", "Subject", "Recent", "Working hours"
            };

        //init first and only datasource, pull
        DataSource ds = DataSource.Instance;
            List = new ObservableCollection<IssueWorktime>();
            List.Add(new IssueWorktime { Tracker = "Feature xy", Project = "ultimate project", Subject = "Documentation", StartTime = DateTime.Now, FinishTime = DateTime.Now.AddDays(2)
            });
        }

        public readonly ObservableCollection<string> OrderingCats;
        private int selectedItem;
        public int SelectedItem { get { return selectedItem; } set { Set(ref selectedItem, value); } }
        public DelegateCommand RefreshCommand { get; }
        public DelegateCommand OrderCommand { get; }
        public void Order()
        {
            switch (SelectedItem)
            {
                case 0:
                    List = new ObservableCollection<IssueWorktime>(List.OrderBy(i => i.Tracker));
                    break;
                case 1:
                    List = new ObservableCollection<IssueWorktime>(List.OrderBy(i => i.Project));
                    break;
                case 2:
                    List = new ObservableCollection<IssueWorktime>(List.OrderBy(i => i.Subject));
                    break;
                case 3:
                    List = new ObservableCollection<IssueWorktime>(List.OrderBy(i => i.StartTime));
                    break;
                case 4:
                    List = new ObservableCollection<IssueWorktime>(List.OrderBy(i => i.FinishTime));
                    break;
            }
            
        }
        public void Refresh()
        {
            List = new ObservableCollection<IssueWorktime>(DataSource.Instance.WorktimeList);
        }
    }
}
