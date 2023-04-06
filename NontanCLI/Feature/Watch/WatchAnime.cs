using NontanCLI.API;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NontanCLI.Models;
using RestSharp;

namespace NontanCLI.Feature.Watch
{
    internal class WatchAnime
    {

        public static RestResponse req;
        public static WatchRoot response;

        [Obsolete]
        public static void WatchAnimeInvoke(string episode_id, string anime_id)
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
                Menu.MenuHandler();
            } else
            {
                foreach (var item in response.sources)
                    {
                    if (item.quality.ToString() == _prompt)
                    {
                        Table table = new Table();
                        table.Title = new TableTitle($"\n[green]Input Key[/]");
                        table.AddColumn("[green]Key[/]");
                        table.AddColumn("[green]Description[/]");


                        table.AddRow("P", "Play");
                        table.AddRow("S", "Stop");
                        table.AddRow("R", "Resume");
                        table.AddRow("Q", "Quit");
                        table.AddRow("Right Arrow", "Seek Forward ( 10 seconds )");
                        table.AddRow("Left Arrow", "Seek Backward ( 10 seconds )");
                        table.AddRow("Up Arrow", "Volume Up");
                        table.AddRow("Down Arrow", "Volume Down");

                        AnsiConsole.Render(table);
                        WatchDirect3D.MediaPlayerInvoke(item.url.ToString(), anime_id);
                    }
                }

            }
        }

        [Obsolete]
        public static void WatchAnimeInvoke(string episode_id)
        {

            try
            {
                req = RestSharpHelper.GetResponse($"meta/anilist/watch/{episode_id}");
                response = JsonConvert.DeserializeObject<WatchRoot>(req.Content);
            }
            catch (Exception ex)
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
                Menu.MenuHandler();
            }
            else
            {
                foreach (var item in response.sources)
                {
                    if (item.quality.ToString() == _prompt)
                    {
                        Table table = new Table();
                        table.Title = new TableTitle($"\n[green]Input Key[/]");
                        table.AddColumn("[green]Key[/]");
                        table.AddColumn("[green]Description[/]");


                        table.AddRow("P", "Play");
                        table.AddRow("S", "Stop");
                        table.AddRow("R", "Resume");
                        table.AddRow("Q", "Quit");
                        table.AddRow("Right Arrow", "Seek Forward ( 10 seconds )");
                        table.AddRow("Left Arrow", "Seek Backward ( 10 seconds )");
                        table.AddRow("Up Arrow", "Volume Up");
                        table.AddRow("Down Arrow", "Volume Down");

                        AnsiConsole.Render(table);
                        WatchDirect3D.MediaPlayerInvoke(item.url.ToString());
                    }
                }

            }
        }
    }
}
