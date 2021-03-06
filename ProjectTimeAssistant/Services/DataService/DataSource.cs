﻿using Microsoft.EntityFrameworkCore;
using ProjectTimeAssistant.Models;
using ProjectTimeAssistant.Services.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using Windows.UI.Popups;

namespace ProjectTimeAssistant.Services.DataService
{
    //thread safe singleton
    class DataSource : IDataService, IDataSyncer
    {
        private static IDataConvertService dataConverter;
        private static DataSource instance = null;

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
                    instance = new DataSource();
                    instance.PullAll();
                }
                    
                return instance;
            }
        }

        //újragondolni miért használom ezt
        List<Issue> issueList;
        List<Project> projectList;
        List<WorkTime> worktimeList;

        public async void PullAll()
        {
            try
            {
                await FetchData();
                using (var db = new DataContext())
                {
                    PullProjects(db);
                    PullIssues(db);
                    PullTimeEntries(db);
                }
            } catch (HttpRequestException e)
            {
                await new MessageDialog("You are currently not connected to the internet. Syncing data failed. You can use offline mode.", "Network Error").ShowAsync();
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
                    exists.FinishTime = timeEntry.FinishTime;

                    var issue = db.Issues.Where(i => i.IssueID == timeEntry.IssueID).Single();
                    exists.Issue = issue;
                    exists.Dirty = false;
                }
            }
            db.SaveChanges();
        }

        public ObservableCollection<Issue> GetIssues()
        {
            using (var db = new DataContext())
            {
                var issues = new ObservableCollection<Issue>( db.Issues.Include(i => i.Project).ToList());
                return issues;
            }
            
        }

        public ObservableCollection<WorkTime> GetWorkTimes()
        {
            using (var db = new DataContext())
            {
                var wts = new ObservableCollection<WorkTime>(db.WorkTimes.Include(wt => wt.Issue).Include(i => i.Issue.Project).OrderByDescending(i => i.StartTime).ToList());
                return wts;
            }

        }

        public void AddTimeEntry(WorkTime workTime)
        {
            using (var db = new DataContext())
            {
                var issue = db.Issues.Where(i => i.IssueID == workTime.IssueID).Single();
                workTime.Issue = issue;
                workTime.Dirty = true;
                db.WorkTimes.Add(workTime);
                db.SaveChanges();
            }

         }

        public void PushAll()
        {
            using (var db = new DataContext())
            {
                var wtList = db.WorkTimes.Include(wt => wt.Issue).Include(i => i.Issue.Project).OrderByDescending(i => i.StartTime).ToList();
                foreach (var wt in wtList)
                {
                    if (wt.Dirty)
                    {
                        wt.Dirty = false;
                        dataConverter.PostTimeEntryAsync(wt);
                    }
                }
                db.SaveChanges();
            }
        }

        public double GetAllWorkingTime(Issue issue)
        {
            using (var db = new DataContext())
            {
                return db.WorkTimes.Where(wt => wt.IssueID == issue.IssueID).Sum(wt => wt.Hours);
            }
        }

        public Issue GetIssueById(int id)
        {
            using (var db = new DataContext())
            {
                return db.Issues.Where(i => i.IssueID == id).Include(i => i.Project).Single();
            }
        }

        public void SetFavourite(int id, bool isFavourite)
        {
            using (var db = new DataContext())
            {
                Issue issue = db.Issues.Where(i => i.IssueID == id).Include(i => i.Project).Single();
                issue.IsFavourite = isFavourite;
                db.SaveChanges();
            }

        }

        public ObservableCollection<Issue> GetFavouriteIssues()
        {
            using (var db = new DataContext())
            {
                return new ObservableCollection<Issue>(db.Issues.Where(i => i.IsFavourite).Include(i => i.Project).ToList());
            }
        }

        public ObservableCollection<WorkTime> GetDirtyWorkTimes()
        {
            using (var db = new DataContext())
            {
                var wts = new ObservableCollection<WorkTime>(db.WorkTimes.Include(wt => wt.Issue).Where(wt => wt.Dirty).Include(i => i.Issue.Project).OrderByDescending(i => i.StartTime).ToList());
                return wts;
            }
        }
    }
}
