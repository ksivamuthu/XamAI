using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Plugin.Media.Abstractions;
using XamAI.Services.Contracts;
using Xamarin.Forms;

namespace XamAI.Services
{
    public class FaceEmotionService : IFaceEmotionService
    {
        readonly static Lazy<FaceAPI> FaceApiClient = new Lazy<FaceAPI>(() =>
        {
            var faceApi = new FaceAPI(new ApiKeyServiceClientCredentials(Settings.FaceApiCredentials));
            faceApi.AzureRegion = Settings.FaceApiRegion;
            return faceApi;
        });

        public async Task<Emotion> GetEmotionAsync(MediaFile mediaFile)
        {
            var stream = mediaFile.GetStream();

            var attributeTypes = new List<FaceAttributeType> { FaceAttributeType.Emotion };
            var faceApiResponseList = await FaceApiClient.Value.Face.DetectWithStreamAsync(stream, returnFaceAttributes: attributeTypes);

            var emotions = faceApiResponseList.Select(x => x.FaceAttributes.Emotion).ToList();
            var emotion = emotions.FirstOrDefault();
            return emotion;
        }
    }
}
