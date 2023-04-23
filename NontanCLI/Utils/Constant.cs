using Newtonsoft.Json;
using NontanCLI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NontanCLI.Utils
{
    public class Constant
    {
        public static readonly string BaseUrl = "https://api.consumet.org/";
        public static readonly string CORS = "https://proxy.vnxservers.com/";

        public static string ConfigPath = "config.json";
        public static string PORT = "";
        public static string PROXY_PORT = "";
        public static string baseAddress = "";
        public static string baseProxyAddress = "";
        public static string provider = "";

        
        public static void InitConfig()
        {
            // Check if config.json file exists
            if (File.Exists(ConfigPath))
            {
                string configJson = File.ReadAllText(ConfigPath);
                ConfigModel config = JsonConvert.DeserializeObject<ConfigModel>(configJson);

                // Access the configuration data
                PORT = config.port;
                PROXY_PORT = config.proxy_port;
                baseAddress = "http://localhost:" + PORT + "/";
                baseProxyAddress = "http://localhost:" + PROXY_PORT + "/";
                provider = config.provider.ToLower();
            } else
            {
                // Create and write JSON content to config.json
                ConfigModel config = new ConfigModel
                {
                    port = "8000",
                    proxy_port = "5001",
                    provider = "gogoanime"
                };
                string configFileContent = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText(ConfigPath, configFileContent);
                Console.WriteLine("config.json file created with default values.");
            }
        }
    }
}
