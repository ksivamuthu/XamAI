using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using ModernHttpClient;
using Newtonsoft.Json;
using XamAI.Model;
using XamAI.Services.Contracts;

namespace XamAI.Services
{
    public class GiphyService : IGiphyService
    {
        readonly HttpClient _client;
        static readonly string GIPHY_RANDOM_URL = "https://api.giphy.com/v1/gifs/random";
        static readonly string GIPHY_SEARCH_URL = "https://api.giphy.com/v1/gifs/search";
        static string AUTH_KEY => Settings.GiphyCredentials;

        public GiphyService()
        {
            _client = new HttpClient(new NativeMessageHandler());
        }

        public async Task<GiphyRandomResult> GetRandomGif(string tag)
        {
            var nvc = new NameValueCollection();
            nvc.Add("api_key", AUTH_KEY);
            nvc.Add("rating", "G");
            if(!string.IsNullOrEmpty(tag))
                nvc.Add("tag", tag);

            var response = await _client.GetStringAsync($"{GIPHY_RANDOM_URL}{UriExtensions.ToQueryString(nvc)}");
            var result = JsonConvert.DeserializeObject<GiphyRandomResult>(response);
            return result;
        }

        public async Task<GiphySearchResult> GetEmotionGifs(string tag)
        {
            var nvc = new NameValueCollection();
            nvc.Add("api_key", AUTH_KEY);
            nvc.Add("rating", "G");
            if(!string.IsNullOrEmpty(tag))
                nvc.Add("q", tag);
            nvc.Add("limit", "30");
            nvc.Add("offset", "0");

            var response = await _client.GetStringAsync($"{GIPHY_SEARCH_URL}{UriExtensions.ToQueryString(nvc)}");
            var result = JsonConvert.DeserializeObject<GiphySearchResult>(response);
            return result;
        }
    }
}
