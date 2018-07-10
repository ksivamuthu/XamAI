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
    public class ComputerVisionPageViewModel : BaseViewModel
    {
        private readonly IVisionService _visionService;
        public ComputerVisionPageViewModel()
        {
            _visionService = new VisionService();
        }

        #region Properties

        public ImageSource ImageSource { get; private set; }

        public string Description { get; private set; }

        public string Tags { get; private set; }

        public bool ResultAvailable => !string.IsNullOrEmpty(Description);


        #endregion

        #region Commands

        public ICommand TakePhotoCommand => new Command(async () => await TakePhoto());

        #endregion

        async Task TakePhoto()
        {
            Description = string.Empty;

            await CrossMedia.Current.Initialize();

            var actionSheetResult = await UserDialogs.Instance.ActionSheetAsync("Computer Vision", "Cancel", null, buttons: new string[] { "Camera", "Gallery" });

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

            var result = await _visionService.DescribeImage(file);
            Description = result.Captions?.OrderByDescending(x => x.Confidence).First().Text;
            Tags = string.Join(", ", result.Tags);
        }
    }
}
