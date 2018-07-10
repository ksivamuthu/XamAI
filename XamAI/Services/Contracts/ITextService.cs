using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Plugin.Media.Abstractions;

namespace XamAI.Services.Contracts
{
    public interface ITextService
    {
        Task<string> GetTextFromHandWrittenText(MediaFile file);

        Task<LanguageBatchResultItem> DetectLanguage(string text);

        Task<EntitiesBatchResultItem> DetectEntities(string text, string language);

        Task<SentimentBatchResultItem> ExtractSentiment(string text, string language);
    }
}