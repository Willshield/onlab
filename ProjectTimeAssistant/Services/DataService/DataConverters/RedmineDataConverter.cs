﻿using System;
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
            //fixme!! container.total_count != container.time_entries.count; ?limit=## parameter needed where ## = total_count
            List<WorkTime> list = new List<WorkTime>();
            for (int i = 0; i < container.time_entries.Length; i++)
            {
                if (i == 20)
                {
                    int a = 10;
                    a++;
                }
                Models.WorkTime tmp = new WorkTime();
                tmp.WorkTimeID = container.time_entries[i].id;
                tmp.Hours = container.time_entries[i].hours;
                tmp.StartTime = (container.time_entries[i].created_on).AddHours(-tmp.Hours);
                tmp.IssueID = container.time_entries[i].issue.id;
                tmp.Comment = container.time_entries[i].comments;
                tmp.Dirty = false;

                list.Add(tmp);
            }

            return list;
        }

        public async Task PostTimeEntryAsync(WorkTime wt)
        {
            Post_Time_Entry pt = new Post_Time_Entry();
            pt.time_entry = new Time_Entry();
            pt.time_entry.issue_id = wt.IssueID;
            pt.time_entry.hours = (float)wt.Hours;
            pt.time_entry.comments = wt.Comment;
            pt.time_entry.activity_id = 1;

            await networkService.PostTimeEntry(pt);
        }

    }
}
