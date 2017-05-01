using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTimeAssistant.Services.Network
{
    interface INetworkDataService
    {
        Task<IssueContainer> GetIssuesAsync();
        Task<ProjectContainer> GetProjectsAsync();
        Task<TimeEntriesContainer> GetTimeEntriesAsync();
        Task PostTimeEntry(Time_Entry t);
    }
}
