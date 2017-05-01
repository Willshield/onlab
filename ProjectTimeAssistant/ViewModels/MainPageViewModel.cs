using Template10.Mvvm;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using ProjectTimeAssistant.Models;
using System.Collections.ObjectModel;
using ProjectTimeAssistant.Services.DataService;

namespace ProjectTimeAssistant.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private string testText;

        IDataService DataService;
        private ObservableCollection<Issue> list;
        public ObservableCollection<Issue> List
        {
            get { return list; }
            set { Set(ref list, value); }
        }


        public MainPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                DataService = new DesignTimeDataService();
                getData();
            }
            else
            {
                DataService = DataSource.Instance;
                getData();
                StartTrackingCommand = new DelegateCommand(StartTracking, CanStartTracking);
            }
        }
        public bool CanStartTracking()
        {
            return !(SelectedIssue == null);
        }

        private void getData()
        {
            List = DataService.GetFavouriteIssues();
        }

        public DelegateCommand StartTrackingCommand { get; }
        public async void StartTracking()
        {
            if (SelectedIssue == null)
                return;

            await DataService.GetIssueById(SelectedIssue.IssueID).StartTracking(null);
            NavigationService.Navigate(typeof(Views.ActuallyTrackingPage));
        }
        private Issue selectedIssue;
        public Issue SelectedIssue {
            get { return selectedIssue; }
            set
            {
                Set(ref selectedIssue, value);
                StartTrackingCommand.RaiseCanExecuteChanged();
            }
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
                    { List = new ObservableCollection<Issue>(List.OrderByDescending(i => i.Updated)); }
                    else
                    { List = new ObservableCollection<Issue>(List.OrderBy(i => i.Updated)); }
                    break;
                case 3:
                    if (byDesc)
                    { List = new ObservableCollection<Issue>(List.OrderByDescending(i => i.AllTrackedTime)); }
                    else
                    { List = new ObservableCollection<Issue>(List.OrderBy(i => i.AllTrackedTime)); }
                    break;
                case 4:
                    if (byDesc)
                    { List = new ObservableCollection<Issue>(List.OrderByDescending(i => i.Description)); }
                    else
                    { List = new ObservableCollection<Issue>(List.OrderBy(i => i.Description)); }
                    break;
            }
        }

        public void Refresh()
        {
            List = DataService.GetFavouriteIssues();
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            Refresh();
            return base.OnNavigatedToAsync(parameter, mode, state);

        }

        //Navigation services
        //string _Value = "Gas";
        //public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        //public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        //{
        //    if (suspensionState.Any())
        //    {
        //        Value = suspensionState[nameof(Value)]?.ToString();
        //    }
        //    await Task.CompletedTask;
        //}

        //public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        //{
        //    if (suspending)
        //    {
        //        suspensionState[nameof(Value)] = Value;
        //    }
        //    await Task.CompletedTask;
        //}

        //public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        //{
        //    args.Cancel = false;
        //    await Task.CompletedTask;
        //}

        //public void GotoDetailsPage() =>
        //    NavigationService.Navigate(typeof(Views.DetailPage), Value);

        //public void GotoSettings() =>
        //    NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        //public void GotoPrivacy() =>
        //    NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        //public void GotoAbout() =>
        //    NavigationService.Navigate(typeof(Views.SettingsPage), 2);

    }
}

