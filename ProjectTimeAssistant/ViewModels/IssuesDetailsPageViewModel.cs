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
    class IssuesDetailsPageViewModel : ViewModelBase
    {
        private ObservableCollection<Issue> list;

        public ObservableCollection<Issue> List
        {
            get { return list; }
            set { Set(ref list, value); }
        }

        public IssuesDetailsPageViewModel()
        {
            RefreshCommand = new DelegateCommand(Refresh);

            //init first and only datasource, pull
            DataSource ds = DataSource.Instance;
            List = new ObservableCollection<Issue>();
            List.Add(new Issue { Tracker = "Feature xy", Project = "ultimate project", Subject = "Documentation", Description = "Paperstuff" });
        }

        public DelegateCommand RefreshCommand { get; }
        public void Refresh()
        {
            List = new ObservableCollection<Issue>(DataSource.Instance.IssueList);
        }

    }
}
