using System.Threading.Tasks;
using XamAI.Model;

namespace XamAI.Services.Contracts
{
    public interface IGiphyService
    {
        Task<GiphyRandomResult> GetRandomGif(string tag);
        Task<GiphySearchResult> GetEmotionGifs(string tag);
    }
}
