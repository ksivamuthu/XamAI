using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Plugin.Media;
using Plugin.Media.Abstractions;
using XamAI.Services;
using XamAI.Services.Contracts;
using Xamarin.Forms;

namespace XamAI.ViewModel
{
    public class TextRecognitionPageViewModel : BaseViewModel
    {
        private ITextService _textService;
        public TextRecognitionPageViewModel()
        {
            _textService = new TextService();
        }

        #region Properties

        public ImageSource ImageSource { get; private set; }

        public string RecognizedText { get; private set; }

        public bool ResultAvailable => !string.IsNullOrEmpty(RecognizedText);

        public string Language { get; private set; }

        public string Entities { get; private set; }

        public string Sentiment { get; private set; }

        #endregion

        #region Commands

        public ICommand TakePhotoCommand => new Command(async () => await TakePhoto());

        #endregion

        async Task TakePhoto()
        {
            RecognizedText = string.Empty;

            await CrossMedia.Current.Initialize();

            var actionSheetResult = await UserDialogs.Instance.ActionSheetAsync("Text Analytics", "Cancel", null, buttons: new string[] { "Camera", "Gallery" });

            MediaFile file = null;

            if(actionSheetResult == "Camera")
            {
                if(!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported) return;

                file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    DefaultCamera = CameraDevice.Rear,
                    PhotoSize = PhotoSize.Medium
                });
            }
            else if(actionSheetResult == "Gallery")
            {
                if(!CrossMedia.Current.IsPickPhotoSupported) return;

                file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions() { ModalPresentationStyle = MediaPickerModalPresentationStyle.OverFullScreen });
            }

            if(file == null)
                return;

            ImageSource = ImageSource.FromStream(() => file.GetStream());

            var text = await _textService.GetTextFromHandWrittenText(file);

            if(text != null)
            {
                RecognizedText = text;

                var languageResult = await _textService.DetectLanguage(RecognizedText);
                var language = languageResult.DetectedLanguages.FirstOrDefault();
                if(language != null)
                {
                    Language = language.Name;
                }

                var entitiesResult = await _textService.DetectEntities(RecognizedText, language.Iso6391Name);
                Entities = string.Join(", ", entitiesResult.Entities.Select(x => x.Name));

                var sentimentResult = await _textService.ExtractSentiment(RecognizedText, language.Iso6391Name);
                Sentiment = DetectSentimentUsingScore(sentimentResult.Score);
            }
        }

        private string DetectSentimentUsingScore(double? score)
        {
            if(score >= 0.7) return "Positive";
            if(score <= 0.3) return "Negative";
            return "Neutral;";
        }
    }
}