using ProjectTimeAssistant.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        ObservableCollection<string> OrderingCats;

        public WorktimePage()
        {
            this.InitializeComponent();

            OrderingCats = new ObservableCollection<string>
            {
                "Issue name", "Project", "Subject", "Recent", "Working hours"
            };

        }

        //private void selectChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ViewModel.Order();
        //}
    }
}
