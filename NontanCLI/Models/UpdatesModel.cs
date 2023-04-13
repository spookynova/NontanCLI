using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NontanCLI.Models
{
    public class UpdatesRoot
    {

        [JsonProperty("whats_new")]
        public List<UpdatesWhatsNew> whats_new { get; set; }
    }

    public class UpdatesWhatsNew
    {
        [JsonProperty("version")]
        public string version { get; set; }

        [JsonProperty("date")]
        public string date { get; set; }

        [JsonProperty("build_version")]
        public string build_version { get; set; }

        [JsonProperty("download_url")]
        public string download_url { get; set; }


        [JsonProperty("vlc_download_url")]
        public string vlc_download_url { get; set; }


        [JsonProperty("changes")]
        public List<string> changes { get; set; }


    }


}
