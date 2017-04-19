using ProjectTimeAssistant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTimeAssistant.Services.DataService
{
    interface IDataConvertService
    {
        Task<List<Issue>> GetIssues();
        Task<List<Project>> GetProjects();
        Task<List<WorkTime>> GetTimeEntries();
        Task<List<IssueWorktime>> GetIssueTimeEntries(); //todo: remove
        
    }
}
