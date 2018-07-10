using XamAI.ViewModel;
using Xamarin.Forms;
using System;

namespace XamAI.Pages
{
    public partial class EmotionGifPage : ContentPage
    {
        public EmotionGifPage()
        {
            InitializeComponent();

            BindingContext = new EmotionGifPageViewModel();
        }

        public EmotionGifPageViewModel ViewModel => BindingContext as EmotionGifPageViewModel;

        public void Handle_Tapped(object sender, EventArgs e)
        {
            ViewModel?.ShowPersonEmotionDetailsCommand?.Execute(null);
        }
    }
}
