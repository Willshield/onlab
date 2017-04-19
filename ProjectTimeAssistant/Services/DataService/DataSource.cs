using Microsoft.EntityFrameworkCore;
using ProjectTimeAssistant.Models;
using ProjectTimeAssistant.Services.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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
        List<Project> projectList;
        List<WorkTime> worktimeList;

        public async void PullAll()
        {
            await FetchData();
            using (var db = new DataContext())
            {
                PullProjects(db);
                PullIssues(db);
                PullTimeEntries(db);
            }
        }

        private async System.Threading.Tasks.Task FetchData()
        {
            projectList = await dataConverter.GetProjects();
            issueList = await dataConverter.GetIssues();
            worktimeList = await dataConverter.GetTimeEntries();
        }

        private void PullProjects(DataContext db)
        {
            foreach (var project in projectList)
            {
                var exists = db.Projects.Where(p => p.ProjectID == project.ProjectID).SingleOrDefault();
                if (exists is null)
                {
                    db.Projects.Add(project);
                }
                else
                {
                    //exists but can be changed
                    exists.ProjectID = project.ProjectID;
                    exists.Name = project.Name;
                    exists.Description = project.Description;
                }
            }
            db.SaveChanges();
        }

        private void PullIssues(DataContext db)
        {
            foreach (var issue in issueList)
            {
                var exists = db.Issues.Where(i => i.IssueID == issue.IssueID).SingleOrDefault();
                if (exists is null)
                {
                    var project = db.Projects.Where(p => p.ProjectID == issue.ProjectID).Single();
                    issue.Project = project;
                    db.Issues.Add(issue);
                }
                else
                {
                    //Todo: exists but can change
                    exists.Updated = issue.Updated;
                    exists.Tracker = issue.Tracker;
                    exists.Subject = issue.Subject;
                    exists.ProjectID = issue.ProjectID;
                    exists.Priority = issue.Priority;
                    exists.IssueID = issue.IssueID;
                    exists.Description = issue.Description;

                    var project = db.Projects.Where(p => p.ProjectID == issue.ProjectID).Single();
                    exists.Project = project;

                    exists.Dirty = false;
                }
            }
            db.SaveChanges();
        }

        private void PullTimeEntries(DataContext db)
        {
            foreach (var timeEntry in worktimeList)
            {
                var exists = db.WorkTimes.Where(w => w.WorkTimeID == timeEntry.WorkTimeID).SingleOrDefault();
                if (exists is null)
                {
                    db.WorkTimes.Add(timeEntry);
                }
                else
                {
                    //exists but can be changed
                    exists.IssueID = timeEntry.IssueID;
                    exists.WorkTimeID = timeEntry.WorkTimeID;
                    exists.StartTime = timeEntry.StartTime;
                    exists.FinishTime = timeEntry.FinishTime; //todo

                    var issue = db.Issues.Where(i => i.IssueID == timeEntry.IssueID).Single();
                    exists.Issue = issue;

                    //Todo: set dirty
                }
            }
            db.SaveChanges();
        }

        public ObservableCollection<Issue> getIssues()
        {
            using (var db = new DataContext())
            {
                var issues = new ObservableCollection<Issue>( db.Issues.Include(i => i.Project).ToList());
                return issues;
            }
            
        }

        public ObservableCollection<WorkTime> getWorkTimes()
        {
            using (var db = new DataContext())
            {
                var wts = new ObservableCollection<WorkTime>(db.WorkTimes.Include(wt => wt.Issue).Include(i => i.Issue.Project).ToList());
                return wts;
            }

        }

        public void PushAll()
        {
            throw new NotImplementedException();
        }

    }
}
