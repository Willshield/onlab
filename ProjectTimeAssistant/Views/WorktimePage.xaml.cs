using ProjectTimeAssistant.Model;
using ProjectTimeAssistant.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectTimeAssistant.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WorktimePage : Page
    {

        List<IssueWorktime> WIssues;
        Profile profile;


        public WorktimePage()
        {
            this.InitializeComponent();

            WIssues = new List<IssueWorktime>
            {
                new IssueWorktime { Tracker = "Feature xy", Project = "ultimate project", Subject = "Documentation", StartTime = DateTime.Now, FinishTime = DateTime.Now.AddDays(2) },
                new IssueWorktime { Tracker = "Loginthingz", Project = "webkit", Subject = "Bugfix", StartTime = DateTime.Now, FinishTime = DateTime.Now.AddDays(2) },
                new IssueWorktime { Tracker = "Easyuse", Project = "trackingapp", Subject = "Feature",  StartTime = DateTime.Now, FinishTime = DateTime.Now.AddDays(2) },
                new IssueWorktime { Tracker = "Otherthing", Project = "webkit", Subject = "Feature",  StartTime = DateTime.Now, FinishTime = DateTime.Now.AddDays(2) },
                new IssueWorktime { Tracker = "Feature xy", Project = "ultimate project", Subject = "Documentation", StartTime = DateTime.Now, FinishTime = DateTime.Now.AddDays(2) },
                new IssueWorktime { Tracker = "Loginthingz", Project = "webkit", Subject = "Bugfix", StartTime = DateTime.Now, FinishTime = DateTime.Now.AddDays(2) },
                new IssueWorktime { Tracker = "Easyuse", Project = "trackingapp", Subject = "Feature",  StartTime = DateTime.Now, FinishTime = DateTime.Now.AddDays(2) },
                new IssueWorktime { Tracker = "Otherthing", Project = "webkit", Subject = "Feature",  StartTime = DateTime.Now, FinishTime = DateTime.Now.AddDays(2) },
                new IssueWorktime { Tracker = "Feature xy", Project = "ultimate project", Subject = "Documentation", StartTime = DateTime.Now, FinishTime = DateTime.Now.AddDays(2) },
                new IssueWorktime { Tracker = "Loginthingz", Project = "webkit", Subject = "Bugfix", StartTime = DateTime.Now, FinishTime = DateTime.Now.AddDays(2) },
                new IssueWorktime { Tracker = "Easyuse", Project = "trackingapp", Subject = "Feature",  StartTime = DateTime.Now, FinishTime = DateTime.Now.AddDays(2) },
                new IssueWorktime { Tracker = "Otherthing", Project = "webkit", Subject = "Feature",  StartTime = DateTime.Now, FinishTime = DateTime.Now.AddDays(2) },
                new IssueWorktime { Tracker = "Feature xy", Project = "ultimate project", Subject = "Documentation", StartTime = DateTime.Now, FinishTime = DateTime.Now.AddDays(2) },
                new IssueWorktime { Tracker = "Loginthingz", Project = "webkit", Subject = "Bugfix", StartTime = DateTime.Now, FinishTime = DateTime.Now.AddDays(2) },
                new IssueWorktime { Tracker = "Easyuse", Project = "trackingapp", Subject = "Feature",  StartTime = DateTime.Now, FinishTime = DateTime.Now.AddDays(2) },
                new IssueWorktime { Tracker = "Otherthing", Project = "webkit", Subject = "Feature",  StartTime = DateTime.Now, FinishTime = DateTime.Now.AddDays(2) }

            };

            profile = new Model.Profile() { Name = "Gáspár Vilmos", Url = "onlab.m.redmine.org" };

        }
    }
}
