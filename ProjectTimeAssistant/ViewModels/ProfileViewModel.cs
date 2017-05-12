using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace ProjectTimeAssistant.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        Services.SettingsServices.SettingsService settingService;
        public DelegateCommand SaveCommand { get; }

        private string profileName;
        public string ProfileName
        {
            get { return profileName == null ? settingService.Profile.Name : profileName; }
            set { Set(ref profileName, value); }
        }
        private string url;
        public string URL
        {
            get { return url == null ? settingService.Profile.Url : url; }
            set { Set(ref url, value); }
        }

        private string key;
        public string Key
        {
            get { return key == null ? settingService.UploadKey : key; }
            set { Set(ref key, value); }
        }

        
        public ProfileViewModel()
        {
            settingService = Services.SettingsServices.SettingsService.Instance;
            SaveCommand = new DelegateCommand(SetProfile);
        }

        public void SetProfile()
        {
            settingService.UploadKey = Key;
            settingService.Profile.Url = URL;
            settingService.Profile.Name = ProfileName;
        }

    }
}
