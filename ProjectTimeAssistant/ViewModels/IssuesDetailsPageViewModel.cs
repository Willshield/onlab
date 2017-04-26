﻿using Microsoft.EntityFrameworkCore;
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

        //Not working!!!
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
            StartTrackingCommand = new DelegateCommand(StartTracking);

            //init first and only datasource to pullAll
            DataSource ds = DataSource.Instance;

            List = new ObservableCollection<Issue>();
            Refresh();
        }

        public DelegateCommand StartTrackingCommand { get; }
        public void StartTracking()
        {
            using( var db = new DataContext())
            {
                Issue issue = db.Issues.Where(i => i.IssueID == 1).Include(i => i.Project).Single();
                Tracker.Instance.StartTracking(issue, "comment");
            }

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
