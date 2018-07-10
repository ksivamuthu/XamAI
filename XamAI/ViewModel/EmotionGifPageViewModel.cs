using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Plugin.Media;
using XamAI.Model;
using XamAI.Services;
using XamAI.Services.Contracts;
using Xamarin.Forms;

namespace XamAI.ViewModel
{
    public class EmotionGifPageViewModel : BaseViewModel
    {
        private readonly IGiphyService _giphyService;
        private readonly IFaceEmotionService _faceEmotionService;

        public EmotionGifPageViewModel()
        {
            _giphyService = new GiphyService();
            _faceEmotionService = new FaceEmotionService();
        }

        #region Properties

        public Emotion Emotion { get; set; }

        public string EmotionDetails { get; set; }

        public GiphySearchResultData RandomGif { get; set; }

        public ImageSource PersonImageSource { get; private set; }

        public string KeyWord { get; set; }

        public ICollection<GiphySearchResultData> EmotionGifs { get; private set; }

        public bool IsGIFLoaded => RandomGif != null;

        public bool ShowMoreGifs { get; private set; } = false;

        public string ToggleMoreText => ShowMoreGifs ? "Hide" : "Show More";

        #endregion

        #region Commands

        public ICommand TakePhotoCommand => new Command(async () => await TakePhoto());

        public ICommand ShowMoreCommand => new Command(() => ShowMoreGifs = !ShowMoreGifs);

        public ICommand ShowPersonEmotionDetailsCommand => new Command(async () =>
        {
            if(Emotion == null) return;
            await UserDialogs.Instance.AlertAsync(EmotionDetails, "Emotion", "Ok");
        });

        #endregion


        async Task GetRandomGif()
        {
            RandomGif = null;

            if(!string.IsNullOrEmpty(KeyWord))
            {
                var queryKeyword = KeyWord.Replace("#", "");
                var gifResult = await _giphyService.GetEmotionGifs(queryKeyword);

                ShowMoreGifs = false;
                EmotionGifs = gifResult.Data.OrderBy(x => new Random().NextDouble()).ToList();
                RandomGif = EmotionGifs.RandomElement();
            }
        }

        async Task TakePhoto()
        {
            RandomGif = null;
            EmotionGifs = null;
            ShowMoreGifs = false;
            PersonImageSource = null;
            Emotion = null;
            EmotionDetails = null;

            await CrossMedia.Current.Initialize();

            if(!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                SaveToAlbum = false,
                DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front,
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });

            if(file == null)
                return;


            PersonImageSource = ImageSource.FromStream(() => file.GetStream());

            Emotion = await _faceEmotionService.GetEmotionAsync(file);

            if(Emotion != null)
            {
                var dict = new Dictionary<string, double>();
                dict[nameof(Emotion.Happiness)] = Emotion.Happiness;
                dict[nameof(Emotion.Anger)] = Emotion.Anger;
                dict[nameof(Emotion.Contempt)] = Emotion.Contempt;
                dict[nameof(Emotion.Disgust)] = Emotion.Disgust;
                dict[nameof(Emotion.Fear)] = Emotion.Fear;
                dict[nameof(Emotion.Neutral)] = Emotion.Neutral;
                dict[nameof(Emotion.Sadness)] = Emotion.Sadness;
                dict[nameof(Emotion.Surprise)] = Emotion.Surprise;

                EmotionDetails = String.Join("\n", dict.Select(x => $"{x.Key} - { x.Value}").ToArray());

                var dominantEmotion = dict.FirstOrDefault(x => x.Value.Equals(dict.Values.Max())).Key;
                KeyWord = dominantEmotion?.ToLower().Trim();

                await GetRandomGif();
            }
        }
    }
}