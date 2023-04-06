using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NontanCLI.Models
{

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Character
        {
            [JsonProperty("id")]
            public int? id { get; set; }

            [JsonProperty("role")]
            public string role { get; set; }

            [JsonProperty("name")]
            public Name name { get; set; }

            [JsonProperty("image")]
            public string image { get; set; }

            [JsonProperty("voiceActors")]
            public List<VoiceActor> voiceActors { get; set; }
        }

        public class EndDate
        {
            [JsonProperty("year")]
            public object year { get; set; }

            [JsonProperty("month")]
            public object month { get; set; }

            [JsonProperty("day")]
            public object day { get; set; }
        }

        public class Episode
        {
            [JsonProperty("id")]
            public string id { get; set; }

            [JsonProperty("title")]
            public string title { get; set; }

            [JsonProperty("description")]
            public string description { get; set; }

            [JsonProperty("number")]
            public int? number { get; set; }

            [JsonProperty("image")]
            public string image { get; set; }

            [JsonProperty("airDate")]
            public DateTime? airDate { get; set; }
        }

        public class Mappings
        {
            [JsonProperty("mal")]
            public int? mal { get; set; }

            [JsonProperty("anidb")]
            public int? anidb { get; set; }

            [JsonProperty("kitsu")]
            public int? kitsu { get; set; }

            [JsonProperty("anilist")]
            public int? anilist { get; set; }

            [JsonProperty("thetvdb")]
            public int? thetvdb { get; set; }

            [JsonProperty("anisearch")]
            public int? anisearch { get; set; }

            [JsonProperty("livechart")]
            public int? livechart { get; set; }

            [JsonProperty("notify.moe")]
            public string notifymoe { get; set; }

            [JsonProperty("anime-planet")]
            public string animeplanet { get; set; }
        }

        public class Name
        {
            [JsonProperty("first")]
            public string first { get; set; }

            [JsonProperty("last")]
            public string last { get; set; }

            [JsonProperty("full")]
            public string full { get; set; }

            [JsonProperty("native")]
            public string native { get; set; }

            [JsonProperty("userPreferred")]
            public string userPreferred { get; set; }
        }

        public class NextAiringEpisode
        {
            [JsonProperty("airingTime")]
            public int? airingTime { get; set; }

            [JsonProperty("timeUntilAiring")]
            public int? timeUntilAiring { get; set; }

            [JsonProperty("episode")]
            public int? episode { get; set; }
        }

        public class Recommendation
        {
            [JsonProperty("id")]
            public int? id { get; set; }

            [JsonProperty("malId")]
            public int? malId { get; set; }

            [JsonProperty("title")]
            public Title title { get; set; }

            [JsonProperty("status")]
            public string status { get; set; }

            [JsonProperty("episodes")]
            public int? episodes { get; set; }

            [JsonProperty("image")]
            public string image { get; set; }

            [JsonProperty("cover")]
            public string cover { get; set; }

            [JsonProperty("rating")]
            public int? rating { get; set; }

            [JsonProperty("type")]
            public string type { get; set; }
        }

        public class Relation
        {
            [JsonProperty("id")]
            public int? id { get; set; }

            [JsonProperty("relationType")]
            public string relationType { get; set; }

            [JsonProperty("malId")]
            public int? malId { get; set; }

            [JsonProperty("title")]
            public Title title { get; set; }

            [JsonProperty("status")]
            public string status { get; set; }

            [JsonProperty("episodes")]
            public object episodes { get; set; }

            [JsonProperty("image")]
            public string image { get; set; }

            [JsonProperty("color")]
            public string color { get; set; }

            [JsonProperty("type")]
            public string type { get; set; }

            [JsonProperty("cover")]
            public string cover { get; set; }

            [JsonProperty("rating")]
            public int? rating { get; set; }
        }

        public class InfoRoot
        {
            [JsonProperty("id")]
            public string id { get; set; }

            [JsonProperty("title")]
            public Title title { get; set; }

            [JsonProperty("malId")]
            public int? malId { get; set; }

            [JsonProperty("synonyms")]
            public List<string> synonyms { get; set; }

            [JsonProperty("isLicensed")]
            public bool? isLicensed { get; set; }

            [JsonProperty("isAdult")]
            public bool? isAdult { get; set; }

            [JsonProperty("countryOfOrigin")]
            public string countryOfOrigin { get; set; }

            [JsonProperty("trailer")]
            public Trailer trailer { get; set; }

            [JsonProperty("image")]
            public string image { get; set; }

            [JsonProperty("popularity")]
            public int? popularity { get; set; }

            [JsonProperty("color")]
            public string color { get; set; }

            [JsonProperty("cover")]
            public string cover { get; set; }

            [JsonProperty("description")]
            public string description { get; set; }

            [JsonProperty("status")]
            public string status { get; set; }

            [JsonProperty("releaseDate")]
            public int? releaseDate { get; set; }

            [JsonProperty("startDate")]
            public StartDate startDate { get; set; }

            [JsonProperty("endDate")]
            public EndDate endDate { get; set; }

            [JsonProperty("nextAiringEpisode")]
            public NextAiringEpisode nextAiringEpisode { get; set; }

            [JsonProperty("totalEpisodes")]
            public int? totalEpisodes { get; set; }

            [JsonProperty("currentEpisode")]
            public int? currentEpisode { get; set; }

            [JsonProperty("rating")]
            public int? rating { get; set; }

            [JsonProperty("duration")]
            public int? duration { get; set; }

            [JsonProperty("genres")]
            public List<string> genres { get; set; }

            [JsonProperty("season")]
            public string season { get; set; }

            [JsonProperty("studios")]
            public List<string> studios { get; set; }

            [JsonProperty("subOrDub")]
            public string subOrDub { get; set; }

            [JsonProperty("type")]
            public string type { get; set; }

            [JsonProperty("recommendations")]
            public List<Recommendation> recommendations { get; set; }

            [JsonProperty("characters")]
            public List<Character> characters { get; set; }

            [JsonProperty("relations")]
            public List<Relation> relations { get; set; }

            [JsonProperty("mappings")]
            public Mappings mappings { get; set; }

            [JsonProperty("episodes")]
            public List<Episode> episodes { get; set; }
        }

        public class StartDate
        {
            [JsonProperty("year")]
            public int? year { get; set; }

            [JsonProperty("month")]
            public int? month { get; set; }

            [JsonProperty("day")]
            public int? day { get; set; }
        }

        public class Title
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

        public class Trailer
        {
            [JsonProperty("id")]
            public string id { get; set; }

            [JsonProperty("site")]
            public string site { get; set; }

            [JsonProperty("thumbnail")]
            public string thumbnail { get; set; }
        }

        public class VoiceActor
        {
            [JsonProperty("id")]
            public int? id { get; set; }

            [JsonProperty("language")]
            public string language { get; set; }

            [JsonProperty("name")]
            public Name name { get; set; }

            [JsonProperty("image")]
            public string image { get; set; }
        }

}
