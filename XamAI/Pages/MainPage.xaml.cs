using System;
using System.Collections.Generic;
using XamAI.ViewModel;
using Xamarin.Forms;

namespace XamAI.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainPageViewModel(Navigation);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}
