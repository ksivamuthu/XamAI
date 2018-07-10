using System;
using Newtonsoft.Json;

namespace XamAI.Model
{
    public class GiphyRandomData
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("image_original_url")]
        public string ImageOriginalUrl { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }
    }

    public class GiphySearchResultData
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("embed_url")]
        public string EmbedUrl { get; set; }

        [JsonProperty("images")]
        public Images Images { get; set; }
    }

    public class Images
    {
        [JsonProperty("original")]
        public Original Original { get; set; }
    }

    public class Original
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
