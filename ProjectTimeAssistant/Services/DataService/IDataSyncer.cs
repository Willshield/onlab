using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTimeAssistant.Services.DataService
{
    interface IDataSyncer
    {
        void PullAll();
        void PushAll();
    }
}
