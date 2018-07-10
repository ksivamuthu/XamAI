using System;
using System.Collections.Generic;
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
    public class CustomVisionPageViewModel : BaseViewModel
    {
        readonly Dictionary<string, string> ClassificationDict = new Dictionary<string, string> { { "seat-belt", "Buckled" }, { "no-seat-belt", "Not Buckled" } };
        readonly IVisionService _visionService;

        public CustomVisionPageViewModel()
        {
            _visionService = new VisionService();
        }

        #region Properties

        public ImageSource ImageSource { get; private set; }

        public string Classification { get; private set; }

        public bool ResultAvailable => !string.IsNullOrEmpty(Classification);

        #endregion

        #region Commands

        public ICommand TakePhotoCommand => new Command(async () => await TakePhoto());

        #endregion

        async Task TakePhoto()
        {
            Classification = string.Empty;
            ImageSource = null;

            await CrossMedia.Current.Initialize();

            var actionSheetResult = await UserDialogs.Instance.ActionSheetAsync("Custom Vision", "Cancel", null, buttons: new string[] { "Camera", "Gallery" });

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

            var classification = await _visionService.ClassifyImage(file);

            string value = string.Empty;
            ClassificationDict.TryGetValue(classification, out value);
            Classification = value;
        }
    }
}
