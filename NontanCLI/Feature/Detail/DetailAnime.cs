using Newtonsoft.Json;
using NontanCLI.API;
using NontanCLI.Feature.Watch;
using NontanCLI.Models;
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
        private RestResponse req;
        private InfoRoot response;

        [Obsolete]
        public void GetDetailParams(string id)
        {

            try
            { 
                req = RestSharpHelper.GetResponse($"/meta/anilist/info/{id}");
                response = JsonConvert.DeserializeObject<InfoRoot>(req.Content);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Table table = new Table();

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
                Thread.Sleep(2000);
                // clear the screen
                AnsiConsole.Clear();
                Program.MenuHandlerInvoke();
                return;
            }

            Console.WriteLine("\n");


            AnsiConsole.MarkupLine("[bold green]Description[/]");
            Console.WriteLine(Regex.Replace(response.description.ToString(), "<.*?>", string.Empty) + "\n");

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


            table.Title = new TableTitle($"\n\n[green]Episode List[/]");
            table.AddColumn("[green]No[/]");
            table.AddColumn("[green]Title[/]");
            table.AddColumn("[green]Description[/]");
            table.AddColumn("[green]Episode ID[/]");

            // Add some rows
            foreach (var item in response.episodes)
            {
                string no = "";
                string title = "";
                string description = "";
                string eps_id = "";
                if (item.id != null)
                {
                    no = item.number.ToString();
                }
                if (item.title != null)
                {
                    title = item.title.ToString();
                }
                if (item.description != null)
                {
                    description = Regex.Replace(item.description.ToString(), "<.*?>", string.Empty);
                
                }

                if (item.id != null)
                {
                    eps_id = item.id.ToString();
                }


                table.AddRow("Episode " + no, title, description  + "\n", eps_id);
            }

            AnsiConsole.Render(table);

            if (response.episodes.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No episode available[/]");

                Thread.Sleep(2000);
                AnsiConsole.Clear();
                Program.MenuHandlerInvoke();
                return;
            }

            var eps = AnsiConsole.Prompt(
                new TextPrompt<int>("What [green]Episode (int)[/] do you want to watch : ")
                    .PromptStyle("green")
                    .ValidationErrorMessage("[red]That's not a valid episode[/]")
                    .Validate(_eps =>
                    {
                        if (_eps > 0 && _eps <= response.episodes.Count)
                        {
                            return ValidationResult.Success();
                        }
                        else
                        {
                            return ValidationResult.Error("Please enter a valid episode");
                        }

                    }));


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
