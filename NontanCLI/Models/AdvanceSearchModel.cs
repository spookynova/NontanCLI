using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NontanCLI.Models
{
    public class AdvanceResultModel
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("malId")]
        public int? malId { get; set; }

        [JsonProperty("title")]
        public Title title { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }

        [JsonProperty("image")]
        public string image { get; set; }

        [JsonProperty("cover")]
        public string cover { get; set; }

        [JsonProperty("popularity")]
        public int? popularity { get; set; }

        [JsonProperty("totalEpisodes")]
        public int? totalEpisodes { get; set; }

        [JsonProperty("currentEpisode")]
        public int? currentEpisode { get; set; }

        [JsonProperty("countryOfOrigin")]
        public string countryOfOrigin { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("genres")]
        public List<string> genres { get; set; }

        [JsonProperty("rating")]
        public int? rating { get; set; }

        [JsonProperty("color")]
        public string color { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("releaseDate")]
        public int? releaseDate { get; set; }
    }

    public class AdvanceRoot
    {
        [JsonProperty("currentPage")]
        public int? currentPage { get; set; }

        [JsonProperty("hasNextPage")]
        public bool? hasNextPage { get; set; }

        [JsonProperty("totalPages")]
        public int? totalPages { get; set; }

        [JsonProperty("totalResults")]
        public int? totalResults { get; set; }

        [JsonProperty("results")]
        public List<AdvanceResultModel> results { get; set; }
    }

    public class AdvanceTitle
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
