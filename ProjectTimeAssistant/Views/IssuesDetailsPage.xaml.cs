using ProjectTimeAssistant.Model;
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
    public sealed partial class IssuesDetailsPage : Page
    {
        List<Issue> Issues;
        Profile profile;

        public IssuesDetailsPage()
        {
            this.InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;

            Issues = new List<Issue>
            {
                new Issue { Tracker = "Feature xy", Project = "ultimate project", Subject = "Documentation", Description = "Paperstuff" },
                new Issue { Tracker = "Loginthingz", Project = "webkit", Subject = "Bugfix", Description = "Users can't login in some cases" },
                new Issue { Tracker = "Easyuse", Project = "trackingapp", Subject = "Feature", Description = "Stuff making easier the app usage" },
                new Issue { Tracker = "Otherthing", Project = "webkit", Subject = "Feature", Description = "Long description, very very very very very very very very very very very loooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooong... And there's more: Long description, very very very very very very very very very very very loooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooong loooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooong... And there's more: Long description, very very very very very very very very very very very loooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooong loooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooong... And there's more: Long description, very very very very very very very very very very very loooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooong" } };

            profile = new Model.Profile() { Name = "Gáspár Vilmos", Url = "onlab.m.redmine.org" };
        }
    }
}
