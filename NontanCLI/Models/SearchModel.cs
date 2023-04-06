using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NontanCLI.Models

{





    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class SearchResultModel
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("malId")]
        public int? malId { get; set; }

        [JsonProperty("title")]
        public SearchTitle title { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }

        [JsonProperty("image")]
        public string image { get; set; }

        [JsonProperty("cover")]
        public string cover { get; set; }

        [JsonProperty("popularity")]
        public int? popularity { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("rating")]
        public int? rating { get; set; }

        [JsonProperty("genres")]
        public List<string> genres { get; set; }

        [JsonProperty("color")]
        public string color { get; set; }

        [JsonProperty("totalEpisodes")]
        public int? totalEpisodes { get; set; }

        [JsonProperty("currentEpisodeCount")]
        public int? currentEpisodeCount { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("releaseDate")]
        public int? releaseDate { get; set; }
    }

    public class SearchRoot
    {
        [JsonProperty("currentPage")]
        public int? currentPage { get; set; }

        [JsonProperty("hasNextPage")]
        public bool? hasNextPage { get; set; }

        [JsonProperty("results")]
        public List<SearchResultModel> results { get; set; }
    }

    public class SearchTitle
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



}
