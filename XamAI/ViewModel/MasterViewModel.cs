using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamAI.ViewModel
{
    public class MasterViewModel : BaseViewModel
    {
        public ObservableCollection<AppMenuItem> MenuItems { get; set; } = new ObservableCollection<AppMenuItem>();

        public MasterViewModel()
        {
            MenuItems.Add(new AppMenuItem { Id = "home", Title = "Home" });
            MenuItems.Add(new AppMenuItem { Id = "about", Title = "About" });
            MenuItems.Add(new AppMenuItem { Id = "settings", Title = "Settings" });
        }

        public event EventHandler<MenuItemEventArgs> MenuSelected;

        public class MenuItemEventArgs : EventArgs
        {
            public MenuItemEventArgs(AppMenuItem item)
            {
                SelectedItem = item;
            }

            public AppMenuItem SelectedItem { get; private set; }
        }

        public ICommand MenuItemSelected => new Command<AppMenuItem>(x => MenuSelected?.Invoke(this, new MenuItemEventArgs(x)));

        public class AppMenuItem
        {
            public string Id { get; set; }
            public string Title { get; set; }
        }
    }
}
