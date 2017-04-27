﻿
using Windows.UI.Xaml.Controls;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace ProjectTimeAssistant.Views.UserControls
{
    public sealed partial class TitledPageHeader : UserControl
    {
        public TitledPageHeader()
        {
            this.InitializeComponent();
        }

        private string label;
        public string Label
        {
            get { return label; }
            set { label = value; }
        }

    }
}
