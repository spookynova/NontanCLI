using Newtonsoft.Json;
using NontanCLI.API;
using NontanCLI.Feature.Watch;
using NontanCLI.Models;
using NontanCLI.Utils;
using RestSharp;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace NontanCLI.Feature.Detail
{
    public class DetailAnime
    {
        private RestResponse? req;
        private InfoRoot? response;

        [Obsolete]
        public void GetDetailParams(string id)
        {

            try
            { 
                req = RestSharpHelper.GetResponse($"/meta/anilist/info/{id}?provider={Constant.provider}");
                response = JsonConvert.DeserializeObject<InfoRoot>(req.Content!);


                // print json

//                Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));


            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Table _table = new Table();
            _table.Border = TableBorder.Ascii2;

            AnsiConsole.MarkupLine("[bold green]Title[/]");
            if (response != null)
            {

                if (response.title.romaji != null)
                {
                    Console.WriteLine("Romaji         : " + response.title.romaji.ToString());
                } else
                {
                    Console.WriteLine("Romaji         : To Be Announce");
                }

                if (response.title.english != null)
                {
                    Console.WriteLine("English        : " + response.title.english.ToString());
                }
                else
                {
                    Console.WriteLine("English        : To Be Announce");
                }

            } else
            {
                AnsiConsole.MarkupLine("[red]Fetching data failed, please try again later[/]");
                // wait for 2 seconds
                Thread.Sleep(5000);
                // clear the screen
                AnsiConsole.Clear();
                Program.MenuHandlerInvoke();
                return;
            }

            Console.WriteLine("\n");


            AnsiConsole.MarkupLine("[bold green]Description[/]");
            if (response.description != null)
            {
                Console.WriteLine(Regex.Replace(response.description.ToString(), "<.*?>", string.Empty) + "\n");

            } else
            {
                Console.WriteLine("There is no description" + "\n");
            }

            AnsiConsole.MarkupLine("[bold green]Type[/]");
            Console.WriteLine(response.type.ToString() + "\n");

            AnsiConsole.MarkupLine("[bold green]Status[/]");
            Console.WriteLine(response.status.ToString() + "\n");

            AnsiConsole.MarkupLine("[bold green]Total Episodes[/]");
            Console.WriteLine(response.totalEpisodes.ToString() + " Episodes \n");

            AnsiConsole.MarkupLine("[bold green]Duration[/]");
            Console.WriteLine(response.duration.ToString() + " Minutes \n");

            AnsiConsole.MarkupLine("[bold green]Genres[/]");
            Console.WriteLine("{0}", string.Join(", ", response.genres) + "\n");

            AnsiConsole.MarkupLine("[bold green]Release Date[/]");
            Console.WriteLine(response.releaseDate.ToString() + "\n");

            AnsiConsole.MarkupLine("[bold green]Studio[/]");
            Console.WriteLine(response.studios[0].ToString() + "\n");


            if (response.episodes.Count == 0)
            {
                AnsiConsole.MarkupLine($"[red]There is no episode available on {Constant.provider} provider, press ENTER to continue..[/]");
                Console.ReadLine();
                AnsiConsole.Clear();
                Program.MenuHandlerInvoke();
                return;
            }


            _table.Title = new TableTitle($"\n\n[green]Episode List[/]");
            _table.AddColumn("[green]No[/]");
            _table.AddColumn("[green]Title[/]");
            _table.AddColumn("[green]Description[/]");
            _table.AddColumn("[green]Episode ID[/]");



            // Add some rows
            foreach (var item in response.episodes)
            {
                string _no = "";
                string _title = "";
                string _description = "";
                string _eps_id = "";
                if (item.id != null)
                {
                    _no = item.number.ToString();
                }
                if (item.title != null)
                {
                    _title = item.title.ToString();
                }
                if (item.description != null)
                {
                    _description = Regex.Replace(item.description.ToString(), "<.*?>", string.Empty);
                
                }

                if (item.id != null)
                {
                    _eps_id = item.id.ToString();
                }


                _table.AddRow("Episode " + _no, _title, _description + "\n", _eps_id);
            }

            AnsiConsole.Render(_table);


            var eps = AnsiConsole.Prompt(
                new TextPrompt<int>("What [green]Episode (int)[/] do you want to watch ( 0 for return to menu previous ): ")
                    .PromptStyle("green")
                    .ValidationErrorMessage("[red]That's not a valid episode[/]")
                    .Validate(_eps =>
                    {
                        if (_eps >= 1 && _eps <= response.episodes.Count)
                        {
                            return ValidationResult.Success();
                        }
                        else
                        {
                            return ValidationResult.Error("Please enter a valid episode");
                        }

                    }));

            if (eps == 0)
            {
                // back to previous menu
            }

            string episode_id = "";
            foreach (var item in response.episodes)
            {
                if (item.number == eps)
                {
                    episode_id = item.id.ToString();
                }
            }
            
            new WatchAnime().WatchAnimeInvoke(episode_id);
        }
    }
}
