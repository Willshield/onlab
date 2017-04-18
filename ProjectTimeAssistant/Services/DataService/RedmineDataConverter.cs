using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTimeAssistant.Models;
using ProjectTimeAssistant.Services.Network;

namespace ProjectTimeAssistant.Services.DataService
{
    class RedmineDataConverter : IDataConvertService
    {

        INetworkDataService networkService;
        public RedmineDataConverter()
        {
            networkService = new RedmineService();
        }

        public async Task<List<Models.Issue>> GetIssues()
        {
            IssueContainer container = await networkService.GetIssuesAsync();

            List<Models.Issue> list = new List<Models.Issue>();
            for (int i = 0; i < container.total_count; i++)
            {
                Models.Issue tmp = new Models.Issue(container.issues[i].id, container.issues[i].tracker.name, container.issues[i].project.name, container.issues[i].subject, container.issues[i].description);
                tmp.Priority = container.issues[i].priority.name;
                tmp.Updated = container.issues[i].updated_on;

                list.Add(tmp);
            }

            return list;
        }

        public async Task<List<Models.Project>> GetProjects()
        {
            ProjectContainer container = await networkService.GetProjectsAsync();

            //TODO: convert to database usable data

            return null;
        }

        public async Task<List<IssueWorktime>> GetTimeEntries()
        {
            TimeEntriesContainer container = await networkService.GetTimeEntriesAsync();

            List<IssueWorktime> list = new List<IssueWorktime>();
            for (int i = 0; i < container.total_count; i++)
            {
                Models.IssueWorktime tmp = new IssueWorktime(container.time_entries[i].issue.id.ToString(), container.time_entries[i].project.name, container.time_entries[i].issue.id.ToString(), container.time_entries[i].updated_on, DateTime.Now);
                
                list.Add(tmp);
            }

            return list;
        }
    }
}
