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

        public async Task<List<Models.Project>> GetProjects()
        {
            ProjectContainer container = await networkService.GetProjectsAsync();

            List<Models.Project> list = new List<Models.Project>();
            for (int i = 0; i < container.total_count; i++)
            {
                Models.Project tmp = new Models.Project();
                tmp.Description = container.projects[i].description;
                tmp.Name = container.projects[i].name;
                tmp.ProjectID = container.projects[i].id;

                list.Add(tmp);
            }

            return list;
        }

        public async Task<List<Models.Issue>> GetIssues()
        {
            IssueContainer container = await networkService.GetIssuesAsync();

            List<Models.Issue> list = new List<Models.Issue>();
            for (int i = 0; i < container.total_count; i++)
            {
                Models.Issue tmp = new Models.Issue();
                tmp.IssueID = container.issues[i].id;
                tmp.Tracker = container.issues[i].tracker.name;
                tmp.Project = new Models.Project();
                tmp.ProjectID = container.issues[i].project.id;
                tmp.Project.Name = container.issues[i].project.name; //removable
                tmp.Subject = container.issues[i].subject;
                tmp.Description = container.issues[i].description;
                tmp.Priority = container.issues[i].priority.name;
                tmp.Updated = container.issues[i].updated_on;

                list.Add(tmp);
            }

            return list;
        }

        public async Task<List<WorkTime>> GetTimeEntries()
        {
            TimeEntriesContainer container = await networkService.GetTimeEntriesAsync();

            List<WorkTime> list = new List<WorkTime>();
            for (int i = 0; i < container.total_count; i++)
            {
                Models.WorkTime tmp = new WorkTime();
                tmp.WorkTimeID = container.time_entries[i].id;
                //tmp.StartTime = container.time_entries[i].
                //todo
                    

                list.Add(tmp);
            }

            return list;
        }

        public async Task<List<IssueWorktime>> GetIssueTimeEntries()
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
