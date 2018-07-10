using System;
using System.Collections.Generic;
using XamAI.ViewModel;
using Xamarin.Forms;

namespace XamAI.Pages
{
    public partial class CustomVisionPage : ContentPage
    {
        public CustomVisionPage()
        {
            InitializeComponent();

            BindingContext = new CustomVisionPageViewModel();
        }
    }
}
