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
using System.Threading.Tasks;

namespace NontanCLI.Feature.Detail
{
    internal class DetailAnime
    {
        public static RestResponse req;
        public static InfoRoot response;

        [Obsolete]
        public static void GetDetailParams(string id)
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

            Console.WriteLine("Id          : " + id);
            Console.WriteLine("Title       : " + response.title.romaji.ToString());
            Console.WriteLine("Description : " + Regex.Replace(response.description.ToString(), "<.*?>", string.Empty));
            Console.WriteLine("Type        : " + response.type.ToString());
            Console.WriteLine("Status      : " + response.status.ToString());



            table.Title = new TableTitle($"\n\n[green]Episode List[/]");
            table.AddColumn("[green]No[/]");
            table.AddColumn("[green]Title[/]");
            table.AddColumn("[green]Description[/]");

            // Add some rows
            foreach (var item in response.episodes)
            {
                string no = "";
                string title = "";
                string description = "";
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
                    var output = description.Substring(0, Math.Min(description.Length, 100));
                    description = output;
                    if (description.Length > 100)
                    {
                        description += "...";
                    }
                }


                table.AddRow("Episode " + no, title, description);
            }

            AnsiConsole.Render(table);

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
            
            WatchAnime.WatchAnimeInvoke(episode_id,id);
        }
    }
}
