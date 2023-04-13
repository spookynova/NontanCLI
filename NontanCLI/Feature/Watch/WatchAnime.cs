using NontanCLI.API;
using Spectre.Console;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NontanCLI.Models;
using RestSharp;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;
namespace NontanCLI.Feature.Watch
{
    internal class WatchAnime
    {

        public static RestResponse req;
        public static WatchRoot response;

        [Obsolete]
        public static void WatchAnimeInvoke(string episode_id)
        {

            try
            {
                req = RestSharpHelper.GetResponse($"meta/anilist/watch/{episode_id}");
                response = JsonConvert.DeserializeObject<WatchRoot>(req.Content);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            // foreach quality

            List<string> quality_selection = new List<string>();


            foreach (var item in response.sources)
            {
                quality_selection.Add(item.quality.ToString());   
            }

            quality_selection.Add("Back");
            var _prompt = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\n[green]Select Quality Anime Video[/]?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more menu)[/]")
                    .AddChoices(quality_selection.ToArray()));

            if (_prompt == "Back")
            {
                new MenuHandler().MenuHandlerInvoke();
            } else
            {
                foreach (var item in response.sources)
                {
                    if (item.quality.ToString() == _prompt)
                    {

                        string CURRENT_DIR = AppDomain.CurrentDomain.BaseDirectory;

                        Process.Start(CURRENT_DIR + "\\vlc\\vlc.exe", item.url.ToString());

                        // check if vlc is playing video

                        Thread.Sleep(5000);
                        while (true)
                        {
                            if (Process.GetProcessesByName("vlc").Length == 0)
                            {
                                Console.Clear();
                                Program.MenuHandlerInvoke();
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
