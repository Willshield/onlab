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

        public static readonly int SubjectKey = 0;
        public static readonly int ProjectNameKey = 1;
        public static readonly int StartTimeKey = 2;
        public static readonly int HoursKey = 3;
        public static readonly int CommentKey = 4;
        public const int NoneKey = 0;
        public const int DayKey = 1;
        public const int WeekKey = 2;
        public const int MonthKey = 3;
        IDataService DataService;

        public WorkTimePageViewModel()
        {
            RefreshCommand = new DelegateCommand(Refresh);
            StartTrackingCommand = new DelegateCommand(StartTracking);
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                DataService = new DesignTimeDataService();
                Refresh();
            } else
            {
                DataService = DataSource.Instance;
                Refresh();
            }

        }

        private int selectedGroupBy;
        public int SelectedGroupBy
        {
            get { return selectedGroupBy; }
            set
            {
                Set(ref selectedGroupBy, value);
                //OrderCatName = StartTimeKey; ToDo: disable order other cats?
                //OrderCats(true);
                GroupItemsBy();
            }
        }

        private void GroupItemsBy()
        {
            if (selectedGroupBy != NoneKey)
            {
                ObservableCollection<WorkTime> newList = new ObservableCollection<WorkTime>();
                WorkTime lastItem = new WorkTime();
                lastItem = List[0];
                switch (selectedGroupBy)
                {
                    case DayKey:
                        newList.Add(CreateDummy(DayKey, lastItem.StartTime.Value.Date));
                        foreach (var item in List)
                        {
                            if(lastItem.StartTime.Value.Date != item.StartTime.Value.Date)
                            {
                                newList.Add(CreateDummy(DayKey, item.StartTime.Value.Date));
                            }
                            newList.Add(item);
                            lastItem = item;
                        }
                        break;
                    case WeekKey:
                        newList.Add(CreateDummy(WeekKey, lastItem.StartTime.Value.Date));
                        foreach (var item in List)
                        {
                            int lastDayNum = (lastItem.StartTime.Value.DayOfWeek.GetHashCode() + 6) % 7;
                            int itemDayNum = (lastItem.StartTime.Value.DayOfWeek.GetHashCode() + 6) % 7;
                            var ts = lastItem.StartTime.Value.Subtract(item.StartTime.Value);
                            if (ts.Days >= 7 || (lastDayNum > itemDayNum && ts.Days < 7))
                            {
                                newList.Add(CreateDummy(WeekKey, item.StartTime.Value.Date));
                            }
                            newList.Add(item);
                            lastItem = item;
                        }
                        break;
                    case MonthKey:
                        newList.Add(CreateDummy(MonthKey, lastItem.StartTime.Value.Date));
                        foreach (var item in List)
                        {
                            if ((lastItem.StartTime.Value.Date.Year == item.StartTime.Value.Date.Year && lastItem.StartTime.Value.Date.Month != item.StartTime.Value.Date.Month) 
                             || (lastItem.StartTime.Value.Date.Year != item.StartTime.Value.Date.Year && lastItem.StartTime.Value.Date.Month != item.StartTime.Value.Date.Month) )
                            {
                                newList.Add(CreateDummy(MonthKey, item.StartTime.Value.Date));
                            }
                            newList.Add(item);
                            lastItem = item;
                        }
                        break;
                    default:
                        return;
                }
                List = newList;

            } else if (selectedGroupBy == NoneKey)
            {
                Refresh();
            } 

        }

        private WorkTime CreateDummy(int GroupByCat, DateTime thisDate)
        {
            WorkTime dummy = new WorkTime();
            dummy.Issue = new Issue();
            dummy.Issue.Project = new Project();
            switch (selectedGroupBy)
            {
                case DayKey:
                    if (thisDate.Date == DateTime.Now.Date)
                    {
                        dummy.Issue.Subject = "Today:";
                    }
                    else
                    {
                        dummy.Issue.Subject = "On " + thisDate.Date.ToString() + ":";
                    }
                    break;
                case WeekKey:
                    //int lastdayNum = (lastDate.DayOfWeek.GetHashCode() + 1) % 7;
                    int dayNum = (thisDate.DayOfWeek.GetHashCode() + 1) % 7;
                    dummy.Issue.Subject = "On the week started on " + thisDate.Date.AddDays(-dayNum).ToString() + ":";
                    break;
                case MonthKey:
                    if (thisDate.Date.Year == DateTime.Now.Year && thisDate.Date.Month == DateTime.Now.Month)
                    {
                        dummy.Issue.Subject = "This month:";
                    }
                    else
                    {
                        dummy.Issue.Subject = "On " + thisDate.Date.Year + "-" + thisDate.Date.Month + ":";
                    }
                    break;
            }

            return dummy;
        }

        private ObservableCollection<WorkTime> list;
        public ObservableCollection<WorkTime> List
        {
            get { return list; }
            set {
                Set(ref list, value);
                }
        }
        public WorkTime SelectedWorkTime { get; set; }
        public DelegateCommand StartTrackingCommand { get; }
        public async void StartTracking()
        {
            if (SelectedWorkTime == null)
                return;

            await DataService.GetIssueById(SelectedWorkTime.IssueID).StartTracking(null);
            NavigationService.Navigate(typeof(Views.ActuallyTrackingPage));
        }
        
        

        public DelegateCommand RefreshCommand { get; }
        public DelegateCommand OrderCommand { get; }
        private int orderCatName;
        public int OrderCatName
        {
            get { return orderCatName; }
            set { orderCatName = value; }
        }
        public void OrderCats(bool byDesc)
        {
            SelectedGroupBy = NoneKey;
            switch (OrderCatName)
            {
                case 0:
                    if (byDesc)
                    { List = new ObservableCollection<WorkTime>(List.OrderByDescending(i => i.Issue.Subject)); }
                    else
                    { List = new ObservableCollection<WorkTime>(List.OrderBy(i => i.Issue.Subject)); }
                    break;
                case 1:
                    if (byDesc)
                    { List = new ObservableCollection<WorkTime>(List.OrderByDescending(i => i.Issue.Project.Name)); }
                    else
                    { List = new ObservableCollection<WorkTime>(List.OrderBy(i => i.Issue.Project.Name)); }
                    break;
                case 2:
                    if (byDesc)
                    { List = new ObservableCollection<WorkTime>(List.OrderByDescending(i => i.StartTime)); }
                    else
                    { List = new ObservableCollection<WorkTime>(List.OrderBy(i => i.StartTime)); }
                    break;
                case 3:
                    if (byDesc)
                    { List = new ObservableCollection<WorkTime>(List.OrderByDescending(i => i.Hours)); }
                    else
                    { List = new ObservableCollection<WorkTime>(List.OrderBy(i => i.Hours)); }
                    break;
                case 4:
                    if (byDesc)
                    { List = new ObservableCollection<WorkTime>(List.OrderByDescending(i => i.Comment)); }
                    else
                    { List = new ObservableCollection<WorkTime>(List.OrderBy(i => i.Comment)); }
                    break;
            }
        }


        public void Refresh()
        {
            List = DataService.GetWorkTimes();
        }
    }
}
