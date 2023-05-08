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

        public static string ConfigPath = "config.json";
        public static string PlayerHtmlPath = @"Plyr/index.html";
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
                ConfigModel config = JsonConvert.DeserializeObject<ConfigModel>(configJson)!;

                // Access the configuration data
                PROXY_PORT = config.proxy_port;
                baseProxyAddress = "http://localhost:" + PROXY_PORT + "/";
                provider = config.provider.ToLower();
            } else
            {
                // Create and write JSON content to config.json
                ConfigModel config = new ConfigModel
                {
                    proxy_port = "5001",
                    provider = "zoro"
                };

                PROXY_PORT = "5001";
                baseProxyAddress = "http://localhost:" + PROXY_PORT + "/";
                provider = "zoro";
                string configFileContent = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText(ConfigPath, configFileContent);
                Console.WriteLine("config.json file created with default values.");
            }
        }
    }
}
