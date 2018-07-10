using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Plugin.Media.Abstractions;
using Xam.Plugins.OnDeviceCustomVision;
using XamAI.Services.Contracts;

namespace XamAI.Services
{
    public class VisionService : IVisionService
    {
        readonly static Lazy<ComputerVisionAPI> lazyVisionApi = new Lazy<ComputerVisionAPI>(() =>
        {
            var visionApi = new ComputerVisionAPI(new ApiKeyServiceClientCredentials(Settings.VisionApiCredentials));
            visionApi.AzureRegion = Settings.VisionApiRegion;
            return visionApi;
        });

        static ComputerVisionAPI visionApi => lazyVisionApi.Value;

        public async Task<ImageAnalysis> AnalyzeImage(MediaFile mediaFile)
        {
            var stream = mediaFile.GetStream();
            var result = await visionApi.AnalyzeImageInStreamAsync(mediaFile.GetStream());
            return result;
        }

        public async Task<string> ClassifyImage(MediaFile mediaFile)
        {
            var classifications = await CrossImageClassifier.Current.ClassifyImage(mediaFile.GetStream());
            var bestPrediction = classifications.OrderByDescending(x => x.Probability).First();
            return bestPrediction.Tag;
        }

        public async Task<ImageDescription> DescribeImage(MediaFile mediaFile)
        {
            var stream = mediaFile.GetStream();
            var result = await visionApi.DescribeImageInStreamAsync(mediaFile.GetStream());
            return result;
        }
    }
}
