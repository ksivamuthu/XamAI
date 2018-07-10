using System;
using System.Collections.Generic;
using XamAI.ViewModel;
using Xamarin.Forms;
using static XamAI.ViewModel.MasterViewModel;

namespace XamAI.Pages
{
    public partial class MenuView : ContentPage
    {
        public MenuView()
        {
            InitializeComponent();

            BindingContext = new MasterViewModel();
        }

        public MasterViewModel ViewModel => BindingContext as MasterViewModel;
    }
}
