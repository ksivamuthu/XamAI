using System;
using Newtonsoft.Json;

namespace XamAI.Model
{
    public class GiphySearchResult
    {
        [JsonProperty("data")]
        public GiphySearchResultData[] Data { get; set; }
    }
}
