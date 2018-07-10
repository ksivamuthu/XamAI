using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using static XamAI.ViewModel.MasterViewModel;

namespace XamAI.Pages
{
    public partial class MasterPage : MasterDetailPage
    {
        public MasterPage()
        {
            InitializeComponent();
            this.MenuView.ViewModel.MenuSelected += (sender, e) => MenuView_MenuSelected(sender, e);
        }

        private string _currentPage;

        void MenuView_MenuSelected(object sender, MenuItemEventArgs e)
        {
            var id = e.SelectedItem?.Id;

            if(_currentPage == id) { IsPresented = false; return; }

            _currentPage = id;

            switch(id)
            {
            case "home":
                MainNavigation.PushAsync(new MainPage());
                break;
            case "about":
                Device.OpenUri(new Uri("https://www.github.com/ksivamuthu/XamAI"));
                break;
            case "settings":
                MainNavigation.PushAsync(new SettingsPage());
                break;
            }
            IsPresented = false;
        }
    }
}
