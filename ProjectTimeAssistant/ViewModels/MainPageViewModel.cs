using Template10.Mvvm;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using ProjectTimeAssistant.Services.Network;

namespace ProjectTimeAssistant.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private string testText;

        public string TestText
        {
            get { return testText; }
            set { Set(ref testText, value); }
        }


        public MainPageViewModel()
        {
            //if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            //{ 
            //}
            getData();

        }

        private void getData()
        {

            //RedmineService networkService = new RedmineService();
            //IssueContainer container = await networkService.GetIssuesAsync();
            //TestText = container.issues[0].tracker.name;
            var _settings = Services.SettingsServices.SettingsService.Instance;
            TestText = _settings.Rounding.ToString();
        }

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

