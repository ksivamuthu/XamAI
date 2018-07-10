using System;
using Newtonsoft.Json;

namespace XamAI.Model
{
    public class GiphyRandomResult
    {
        [JsonProperty("data")]
        public GiphyRandomData Data { get; set; }
    }
}
