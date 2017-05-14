using System;
using Template10.Common;
using Template10.Utils;
using Windows.UI.Xaml;

namespace ProjectTimeAssistant.Services.SettingsServices
{
    public class SettingsService
    {
        public static SettingsService Instance { get; } = new SettingsService();
        Template10.Services.SettingsService.ISettingsHelper _helper;
        private SettingsService()
        {
            _helper = new Template10.Services.SettingsService.SettingsHelper();

            //todo: load it
            //Profile = new Models.Profile() { Name = "Gáspár Vilmos", Url = "onlab.m.redmine.org" };
        }

        public bool UseShellBackButton
        {
            get { return _helper.Read<bool>(nameof(UseShellBackButton), true); }
            set
            {
                _helper.Write(nameof(UseShellBackButton), value);
                BootStrapper.Current.NavigationService.GetDispatcherWrapper().Dispatch(() =>
                {
                    BootStrapper.Current.ShowShellBackButton = value;
                    BootStrapper.Current.UpdateShellBackButton();
                });
            }
        }

        public ApplicationTheme AppTheme
        {
            get
            {
                var theme = ApplicationTheme.Light;
                var value = _helper.Read<string>(nameof(AppTheme), theme.ToString());
                return Enum.TryParse<ApplicationTheme>(value, out theme) ? theme : ApplicationTheme.Dark;
            }
            set
            {
                _helper.Write(nameof(AppTheme), value.ToString());
                (Window.Current.Content as FrameworkElement).RequestedTheme = value.ToElementTheme();
                Views.Shell.HamburgerMenu.RefreshStyles(value, true);
            }
        }

        public TimeSpan CacheMaxDuration
        {
            get { return _helper.Read<TimeSpan>(nameof(CacheMaxDuration), TimeSpan.FromDays(2)); }
            set
            {
                _helper.Write(nameof(CacheMaxDuration), value);
                BootStrapper.Current.CacheMaxDuration = value;
            }
        }

        public bool ShowHamburgerButton
        {
            get { return _helper.Read<bool>(nameof(ShowHamburgerButton), true); }
            set
            {
                _helper.Write(nameof(ShowHamburgerButton), value);
                Views.Shell.HamburgerMenu.HamburgerButtonVisibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public bool IsFullScreen
        {
            get { return _helper.Read<bool>(nameof(IsFullScreen), false); }
            set
            {
                _helper.Write(nameof(IsFullScreen), value);
                Views.Shell.HamburgerMenu.IsFullScreen = value;
            }
        }

        /// <summary>
        /// saját hozzáadások
        /// </summary>
        public Models.Profile Profile
        {
            get
            {
                string name = _helper.Read<string>("ProfileName", "Gáspár Vilmos");
                string url = _helper.Read<string>("URL", "Gáspár Vilmos");
                return new Models.Profile() { Name = name, Url = url };
            }
            set
            {
                _helper.Write("ProfileName", value.Name);
                _helper.Write("URL", value.Url);
            }
        }

        public int Rounding
        {
            get { return _helper.Read<int>(nameof(Rounding), 30); }
            set { _helper.Write(nameof(Rounding), value); }
        }

        public bool AutoTrack {
            get { return _helper.Read<bool>(nameof(AutoTrack), false); }
            set { _helper.Write(nameof(AutoTrack), value); }
        }

        public bool AskIfStop
        {
            get { return _helper.Read<bool>(nameof(AskIfStop), false); }
            set { _helper.Write(nameof(AskIfStop), value); }
        }

        public bool AlwaysUp
        {
            get { return _helper.Read<bool>(nameof(AlwaysUp), true); }
            set { _helper.Write(nameof(AlwaysUp), value); }
        }

        public string UploadKey
        {
            get { return _helper.Read<string>(nameof(UploadKey), "4f56fb8188c5f48811efe9a47b7ef50ad3443318"); }
            set { _helper.Write(nameof(UploadKey), value); }
        }
        

    }
}

