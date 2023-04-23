using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NontanCLI.Models
{
    public class ConfigModel
    {

        [JsonProperty("port")]
        public string port { get; set; }

        [JsonProperty("proxy_port")]
        public string proxy_port { get; set; }

        [JsonProperty("provider")]
        public string provider { get; set; }
    }
}
