using Newtonsoft.Json;

namespace NontanCLI.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AiringScheduleResultModel
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("malId")]
        public int? malId { get; set; }

        [JsonProperty("episode")]
        public int? episode { get; set; }

        [JsonProperty("airingAt")]
        public int? airingAt { get; set; }

        [JsonProperty("title")]
        public AiringScheduleTitle title { get; set; }

        [JsonProperty("country")]
        public string country { get; set; }

        [JsonProperty("image")]
        public string image { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("cover")]
        public string cover { get; set; }

        [JsonProperty("genres")]
        public List<string> genres { get; set; }

        [JsonProperty("color")]
        public string color { get; set; }

        [JsonProperty("rating")]
        public int? rating { get; set; }

        [JsonProperty("releaseDate")]
        public int? releaseDate { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }
    }

    public class AiringScheduleRoot
    {
        [JsonProperty("currentPage")]
        public int? currentPage { get; set; }

        [JsonProperty("hasNextPage")]
        public bool? hasNextPage { get; set; }

        [JsonProperty("results")]
        public List<AiringScheduleResultModel> results { get; set; }
    }

    public class AiringScheduleTitle
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
