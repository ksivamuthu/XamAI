using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Plugin.Media.Abstractions;
using XamAI.Services.Contracts;

namespace XamAI.Services
{
    public class TextService : ITextService
    {
        readonly static Lazy<ComputerVisionAPI> lazyComputerVisionApi =
            new Lazy<ComputerVisionAPI>(() =>
            {
                var api = new ComputerVisionAPI(new ApiKeyServiceClientCredentials(Settings.VisionApiCredentials), handlers: null)
                {
                    AzureRegion = Settings.VisionApiRegion
                };
                return api;
            });

        static ComputerVisionAPI visionApi => lazyComputerVisionApi.Value;

        readonly static Lazy<TextAnalyticsAPI> lazyTextAnalyticsApi = new Lazy<TextAnalyticsAPI>(() =>
        {
            var api = new TextAnalyticsAPI(new ApiKeyServiceClientCredentials(Settings.TextAnalyticsApiCredentials), handlers: null)
            {
                AzureRegion = Settings.TextAnalyticsApiRegion
            };
            return api;
        });

        static TextAnalyticsAPI textAnalyticsApi => lazyTextAnalyticsApi.Value;

        public async Task<string> GetTextFromHandWrittenText(MediaFile file)
        {
            var stream = file.GetStream();
            var headers = await visionApi.RecognizeTextInStreamAsync(stream, TextRecognitionMode.Handwritten);
            var id = headers.OperationLocation.Split('/').Last();
            TextOperationResult result;
            do
            {
                result = await visionApi.GetTextOperationResultAsync(id);
            }
            while(result.Status == TextOperationStatusCodes.Running);

            if(result.Status == TextOperationStatusCodes.Succeeded && result.RecognitionResult != null && result.RecognitionResult.Lines != null && result.RecognitionResult.Lines.Any())
            {
                return string.Join("\n", result.RecognitionResult.Lines.Select(l => l.Text));
            }

            return null;
        }

        public async Task<LanguageBatchResultItem> DetectLanguage(string text)
        {
            var inputList = new List<Input> { new Input("1", text) };
            var result = await textAnalyticsApi.DetectLanguageAsync(new BatchInput(inputList));
            return result.Documents.FirstOrDefault();
        }

        public async Task<EntitiesBatchResultItem> DetectEntities(string text, string language)
        {
            var inputList = new List<MultiLanguageInput> { new MultiLanguageInput(language, "1", text) };
            var result = await textAnalyticsApi.EntitiesAsync(new MultiLanguageBatchInput(inputList));
            return result.Documents.FirstOrDefault();
        }

        public async Task<SentimentBatchResultItem> ExtractSentiment(string text, string language)
        {
            var inputList = new List<MultiLanguageInput> { new MultiLanguageInput(language, "1", text) };
            var result = await textAnalyticsApi.SentimentAsync(new MultiLanguageBatchInput(inputList));
            return result.Documents.FirstOrDefault();
        }
    }
}
