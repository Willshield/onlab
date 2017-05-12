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
        private string profileName;
        public string ProfileName
        {
            get { return profileName; }
            set { profileName = value; }
        }
        private string url;

        public string URL
        {
            get { return url; }
            set { url = value; }
        }

        public ProfileViewModel()
        {

        }

        public void SetProfile()
        {
            int i = 0;
            i++;
        }

    }
}
