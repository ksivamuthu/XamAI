using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Plugin.Media.Abstractions;

namespace XamAI.Services.Contracts
{
    public interface IFaceEmotionService
    {
        Task<Emotion> GetEmotionAsync(MediaFile mediaFile);
    }
}
