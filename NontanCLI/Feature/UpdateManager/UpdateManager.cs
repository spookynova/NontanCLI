using Newtonsoft.Json;
using NontanCLI.API;
using NontanCLI.Models;
using RestSharp;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NontanCLI.Feature.UpdateManager
{
    internal class UpdateManager
    {
        public static UpdatesRoot response;

        public static void UpdateManagerInvoke()
        {
            try
            {
                var client = new RestClient("https://raw.githubusercontent.com/");
                var request = new RestRequest("evnx32/NontanCLI/main/updates.json", Method.Get);

                RestResponse req = client.Execute(request);

                response = JsonConvert.DeserializeObject<UpdatesRoot>(req.Content);

                if (response != null)
                {
                    if (response.whats_new != null)
                    {

                        AnsiConsole.MarkupLine($"[bold white]New Version      : [/] [bold green]{response.whats_new[0].version}[/]" + $" ({response.whats_new[0].build_version})");

                        if (response.whats_new[0].build_version != Program.buildVersion)
                        {
                            AnsiConsole.MarkupLine($"[bold yellow]New Update Available[/]");
                            AnsiConsole.MarkupLine($"[bold white]New Version      : [/] [bold green]{response.whats_new[0].version}[/]" + $" ({response.whats_new[0].build_version})");
                            AnsiConsole.MarkupLine($"[bold white]Current Version  : [/] [bold green]{Program.version}[/]" + $" ({Program.buildVersion})");
                            AnsiConsole.MarkupLine($"[bold white]Date Release     : [/] [bold green]{response.whats_new[0].date}[/]");
                            AnsiConsole.MarkupLine($"[bold white]What's New[/]");
                            foreach (var item in response.whats_new[0].changes)
                            {
                                AnsiConsole.MarkupLine($"[bold green] + {item}[/] ");
                            }

                            AnsiConsole.MarkupLine($"[bold white]Download URL     : [/] [bold green]{response.whats_new[0].download_url}[/]");

                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
