using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NontanCLI.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class RecentResultModel
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("malId")]
        public int? malId { get; set; }

        [JsonProperty("title")]
        public Title title { get; set; }

        [JsonProperty("image")]
        public string image { get; set; }

        [JsonProperty("rating")]
        public int? rating { get; set; }

        [JsonProperty("color")]
        public string color { get; set; }

        [JsonProperty("episodeId")]
        public string episodeId { get; set; }

        [JsonProperty("episodeTitle")]
        public string episodeTitle { get; set; }

        [JsonProperty("episodeNumber")]
        public int? episodeNumber { get; set; }

        [JsonProperty("genres")]
        public List<string> genres { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }
    }

    public class RecentRoot
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
        public List<RecentResultModel> results { get; set; }
    }

    public class RecentTitle
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
