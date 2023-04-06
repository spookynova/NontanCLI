using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NontanCLI.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class TrendingResultModel
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("malId")]
        public int? malId { get; set; }

        [JsonProperty("title")]
        public Title title { get; set; }

        [JsonProperty("image")]
        public string image { get; set; }

        [JsonProperty("trailer")]
        public Trailer trailer { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }

        [JsonProperty("cover")]
        public string cover { get; set; }

        [JsonProperty("rating")]
        public int? rating { get; set; }

        [JsonProperty("releaseDate")]
        public int? releaseDate { get; set; }

        [JsonProperty("color")]
        public string color { get; set; }

        [JsonProperty("genres")]
        public List<string> genres { get; set; }

        [JsonProperty("totalEpisodes")]
        public int? totalEpisodes { get; set; }

        [JsonProperty("duration")]
        public int? duration { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }
    }

    public class TrendingRoot
    {
        [JsonProperty("currentPage")]
        public int? currentPage { get; set; }

        [JsonProperty("hasNextPage")]
        public bool? hasNextPage { get; set; }

        [JsonProperty("results")]
        public List<TrendingResultModel> results { get; set; }
    }

    public class TrendingTitle
    {
        [JsonProperty("romaji")]
        public string romaji { get; set; }

        [JsonProperty("english")]
        public string english { get; set; }

        [JsonProperty("native")]
        public string native { get; set; }

        [JsonProperty("userPreferred")]
        public string userPreferred { get; set; }
    }

    public class TrendingTrailer
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("site")]
        public string site { get; set; }

        [JsonProperty("thumbnail")]
        public string thumbnail { get; set; }
    }


}
