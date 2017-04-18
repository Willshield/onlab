using ProjectTimeAssistant.Models;
using System;
using System.Collections.Generic;


namespace ProjectTimeAssistant.Services.DataService
{
    //thread safe singleton
    class DataSource
    {
        private static IDataConvertService dataConverter;
        private static DataSource instance = null;
        private static readonly object padlock = new object();

        DataSource()
        {
            dataConverter = new RedmineDataConverter();
        }

        public static DataSource Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new DataSource();
                            //todo
                            instance.PullAll();
                        }
                    }
                }
                return instance;
            }
        }

        List<Issue> issueList;
        public List<Issue> IssueList { get { return issueList; } private set { issueList = value; } }
        List<Project> projectList;
        public List<Project> ProjectList { get { return projectList; } private set { projectList = value; } }
        List<IssueWorktime> worktimeList;
        public List<IssueWorktime> WorktimeList { get { return worktimeList; } private set { worktimeList = value; } }
        public async void PullAll()
        {
            issueList = await dataConverter.GetIssues();
            worktimeList = await dataConverter.GetTimeEntries();
        }

        public void PushAll()
        {
            throw new NotImplementedException();
        }

    }
}
