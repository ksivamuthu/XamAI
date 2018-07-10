using XamAI.ViewModel;
using Xamarin.Forms;

namespace XamAI.Pages
{
    public partial class TextRecognitionPage : ContentPage
    {
        public TextRecognitionPage()
        {
            InitializeComponent();

            BindingContext = new TextRecognitionPageViewModel();
        }
    }
}
