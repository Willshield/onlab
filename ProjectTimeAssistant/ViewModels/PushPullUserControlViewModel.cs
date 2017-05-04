using ProjectTimeAssistant.Services.DataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace ProjectTimeAssistant.ViewModels
{
    public class PushPullUserControlViewModel : ViewModelBase
    {
        IDataSyncer Syncer;
        public DelegateCommand PushCommand { get; }
        public PushPullUserControlViewModel()
        {
            Syncer = DataSource.Instance;
            PushCommand = new DelegateCommand(Push);
        }
        public void Push()
        {
            Syncer.PushAll();
        }


    }
}
