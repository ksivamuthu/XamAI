using XamAI.ViewModel;
using Xamarin.Forms;

namespace XamAI.Pages
{
    public partial class ComputerVisionPage : ContentPage
    {
        public ComputerVisionPage()
        {
            InitializeComponent();

            BindingContext = new ComputerVisionPageViewModel();
        }
    }
}
