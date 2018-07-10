using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace XamAI
{
    public static class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;

        static Settings()
        {
## Set API credentials here
## Get the keys from https://azure.microsoft.com/en-us/services/cognitive-services/ and https://developers.giphy.com
            FaceApiCredentials = "";
            VisionApiCredentials = "";
            TextAnalyticsApiCredentials = "";
            GiphyCredentials = "";
            FaceApiRegion = Microsoft.Azure.CognitiveServices.Vision.Face.Models.AzureRegions.Eastus;
            VisionApiRegion = Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models.AzureRegions.Eastus;
            TextAnalyticsApiRegion = Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models.AzureRegions.Eastus;
        }

        public static string FaceApiCredentials
        {
            get { return AppSettings.GetValueOrDefault(nameof(FaceApiCredentials), null); }
            set { AppSettings.AddOrUpdateValue(nameof(FaceApiCredentials), value); }
        }

        public static Microsoft.Azure.CognitiveServices.Vision.Face.Models.AzureRegions FaceApiRegion { get; set; }

        public static string VisionApiCredentials
        {
            get { return AppSettings.GetValueOrDefault(nameof(VisionApiCredentials), null); }
            set { AppSettings.AddOrUpdateValue(nameof(VisionApiCredentials), value); }
        }

        public static Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models.AzureRegions VisionApiRegion { get; set; }

        public static string TextAnalyticsApiCredentials
        {
            get { return AppSettings.GetValueOrDefault(nameof(TextAnalyticsApiCredentials), null); }
            set { AppSettings.AddOrUpdateValue(nameof(TextAnalyticsApiCredentials), value); }
        }

        public static Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models.AzureRegions TextAnalyticsApiRegion { get; set; }

        public static string GiphyCredentials
        {
            get { return AppSettings.GetValueOrDefault(nameof(GiphyCredentials), null); }
            set { AppSettings.AddOrUpdateValue(nameof(GiphyCredentials), value); }
        }
    }
}
