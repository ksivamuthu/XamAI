using System.Collections.ObjectModel;
using System.Windows.Input;
using XamAI.Pages;
using Xamarin.Forms;

namespace XamAI.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        private INavigation _navigation;
        public MainPageViewModel(INavigation navigation)
        {
            _navigation = navigation;

            CognitiveServices.Add(new AzureCogntiveService { Id = 1, Name = "Computer Vision", Service = CognitiveService.Vision, Description = "Distill actionable information from images" });
            CognitiveServices.Add(new AzureCogntiveService { Id = 2, Name = "Custom Vision", Service = CognitiveService.Vision, Description = "Easily customize your own state-of-the-art computer vision models for your unique use case" });
            CognitiveServices.Add(new AzureCogntiveService { Id = 3, Name = "Face", Service = CognitiveService.Vision, Description = "Detect, identify, analyze faces in photos" });
            CognitiveServices.Add(new AzureCogntiveService { Id = 4, Name = "Text Analytics", Service = CognitiveService.Search, Description = "Easily evaluate sentiment and topics to understand what users want" });
        }

        public ObservableCollection<AzureCogntiveService> CognitiveServices { get; } = new ObservableCollection<AzureCogntiveService>();

        public ICommand CognitiveServiceSelected => new Command<AzureCogntiveService>((args) => NavigateToPage(args));

        private void NavigateToPage(AzureCogntiveService item)
        {
            if(item.Id == 1)
            {
                _navigation.PushAsync(new ComputerVisionPage());
            }
            else if(item.Id == 2)
            {
                _navigation.PushAsync(new CustomVisionPage());
            }
            else if(item.Id == 3)
            {
                _navigation.PushAsync(new EmotionGifPage());
            }
            else if(item.Id == 4)
            {
                _navigation.PushAsync(new TextRecognitionPage());
            }
        }
    }
}