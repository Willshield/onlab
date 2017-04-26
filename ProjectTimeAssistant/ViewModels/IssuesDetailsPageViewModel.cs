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

        private double allWorkingTime;
        public double AllWorkingTimes
        {
            get { return allWorkingTime; }
            set { Set(ref allWorkingTime, value); }
        }

        private object selected;
        public object Selected
        {
            get { return selected; }
            set
            {
                Set(ref selected, value);
                AllWorkingTimes = DataSource.Instance.getAllWorkingTime(value as Issue);
            }
        }
        
        public IssuesDetailsPageViewModel()
        {
            RefreshCommand = new DelegateCommand(Refresh);
            PullCommand = new DelegateCommand(PullAll); //todo: átrakni usercontrolhoz

            //init first and only datasource to pullAll
            DataSource ds = DataSource.Instance;

            List = new ObservableCollection<Issue>();
            Refresh();
        }

        public DelegateCommand RefreshCommand { get; }
        public void Refresh()
        {
            List = DataSource.Instance.getIssues();
        }

        public DelegateCommand PullCommand { get; }
        public void PullAll()
        {
            DataSource.Instance.PullAll();
        }


    }

}
