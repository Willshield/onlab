using System;
using ProjectTimeAssistant.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using ProjectTimeAssistant.Model;
using System.Collections.Generic;

namespace ProjectTimeAssistant.Views
{
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;

        }
    }
}
