using Microsoft.EntityFrameworkCore;
using ProjectTimeAssistant.Models;
using ProjectTimeAssistant.Services.DataBase;
using ProjectTimeAssistant.Services.DataService;
using ProjectTimeAssistant.Services.Tracking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace ProjectTimeAssistant.ViewModels
{
    class IssuesDetailsPageViewModel : ViewModelBase
    {

        public static readonly int SubjectKey = 0;
        public static readonly int ProjectNameKey = 1;
        public static readonly int TrackerKey = 2;

        private ObservableCollection<Issue> list;
        public ObservableCollection<Issue> List
        {
            get { return list; }
            set { Set(ref list, value); }
        }

        IDataService DataService;
        
        public IssuesDetailsPageViewModel()
        {

            DataService = DataSource.Instance;

            List = new ObservableCollection<Issue>();
            Refresh();
        }

        public void Refresh()
        {
            List = DataSource.Instance.GetIssues();
        }

        private int orderCatName;
        public int OrderCatName
        {
            get { return orderCatName; }
            set { orderCatName = value; }
        }
        public void OrderCats(bool byDesc)
        {
            switch (OrderCatName)
            {
                case 0:
                    if (byDesc)
                    { List = new ObservableCollection<Issue>(List.OrderByDescending(i => i.Subject)); }
                    else
                    { List = new ObservableCollection<Issue>(List.OrderBy(i => i.Subject)); }
                    break;
                case 1:
                    if (byDesc)
                    { List = new ObservableCollection<Issue>(List.OrderByDescending(i => i.Project.Name)); }
                    else
                    { List = new ObservableCollection<Issue>(List.OrderBy(i => i.Project.Name)); }
                    break;
                case 2:
                    if (byDesc)
                    { List = new ObservableCollection<Issue>(List.OrderByDescending(i => i.Tracker)); }
                    else
                    { List = new ObservableCollection<Issue>(List.OrderBy(i => i.Tracker)); }
                    break;
            }
        }

    }

}
