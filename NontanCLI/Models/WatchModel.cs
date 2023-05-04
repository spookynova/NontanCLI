using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NontanCLI.Models
{
    public class WatchHeaders
    {
        [JsonProperty("Referer")]
        public string? Referer { get; set; }
    }

    public class WatchRoot
    {
        [JsonProperty("headers")]
        public WatchHeaders? headers { get; set; }

        [JsonProperty("sources")]
        public List<WatchSource>? sources { get; set; }

        [JsonProperty("download")]
        public string? download { get; set; }

        [JsonProperty("subtitles")]
        public List<WatchSubtitle>? subtitles { get; set; }

        [JsonProperty("intro")]
        public WatchIntro? intro { get; set; }

        [JsonProperty("message")]
        public string message { get; set; }

    }

    public class WatchSource
    {
        [JsonProperty("url")]
        public string? url { get; set; }

        [JsonProperty("isM3U8")]
        public bool? isM3U8 { get; set; }

        [JsonProperty("quality")]
        public string? quality { get; set; }
    }


    public class WatchSubtitle
    {
        [JsonProperty("url")]
        public string? url { get; set; }

        [JsonProperty("lang")]
        public string? lang { get; set; }
    }

    public class WatchIntro
    {
        [JsonProperty("start")]
        public int? start { get; set; }

        [JsonProperty("end")]
        public int? end { get; set; }
    }
}
