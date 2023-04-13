using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NontanCLI.API;
using RestSharp;
using Spectre.Console;
using NontanCLI.Models;
using Newtonsoft.Json;
using System.Collections;
using NontanCLI.Feature.Detail;
using System.Text.RegularExpressions;

namespace NontanCLI.Feature.Popular
{
    public class PopularAnime
    {
        private int page = 1;

        private RestResponse req;
        private PopularRoot response;

        [Obsolete]
        public void PopularAnimeInvoke()
        {
            Table table = new Table();


            try
            {
                req = RestSharpHelper.GetResponse($"meta/anilist/popular?page={page}");
                response = JsonConvert.DeserializeObject<PopularRoot>(req.Content);

            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            if (response != null)
            {

                table.Title = new TableTitle($"\n\n[green]Popular Anime Page {response.currentPage}[/]");
                table.AddColumn("[green]ID[/]");
                table.AddColumn("[green]Title[/]");
                table.AddColumn("[green]Status[/]");
                table.AddColumn("[green]Type[/]");
                table.AddColumn("[green]Rating[/]");
                List<string> list_name = new List<string>();
                List<PopularResultModel> popular_list = new List<PopularResultModel>();
                foreach (var item in response.results)
                {
                    popular_list.Add(item);

                    string id = "";
                    string title = "";
                    string status = "";
                    string type = "";
                    string rating = "";
                    if (item.id != null)
                    {
                        id = item.id.ToString();
                    }
                    if (item.title.romaji != null)
                    {

                        title = Regex.Replace(item.title.romaji.ToString(), "[^a-zA-Z ]", string.Empty);
                        list_name.Add(Regex.Replace(item.title.romaji.ToString(), "[^a-zA-Z ]", string.Empty));

                    }
                    else if (item.title.english != null)
                    {
                        title = Regex.Replace(item.title.english.ToString(), "[^a-zA-Z ]", string.Empty);
                        list_name.Add(Regex.Replace(item.title.english.ToString(), "[^a-zA-Z ]", string.Empty));

                    }
                    if (item.status != null)
                    {
                        status = item.status.ToString();
                    }
                    if (item.type != null)
                    {
                        type = item.type.ToString();
                    }
                    if (item.rating != null)
                    {
                        rating = item.rating.ToString();
                    }


                    if (status == "Completed")
                    {
                        table.AddRow(id, title, "[green]" + status + "[/]", type, rating);

                    }
                    else if (status == "Ongoing")
                    {
                        table.AddRow(id, title, "[yellow]" + status + "[/]", type, rating);

                    }
                }

                AnsiConsole.Render(table);

                MenuPrompt:

                string _prompt = "";
                if (page > 1)
                {
                    _prompt = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]Select Menu Available[/]?")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Move up and down to reveal more menu)[/]")
                        .AddChoices(new[] {
                                                        "Select Anime","Next Page","Previous Page","Back"
                        }));
                } else
                {


                    _prompt = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("[green]Select Menu Available[/]?")
                            .PageSize(10)
                            .MoreChoicesText("[grey](Move up and down to reveal more menu)[/]")
                            .AddChoices(new[] {
                                                            "Select Anime","Next Page","Back"
                            }));
                }



                if (_prompt == "Select Anime")
                {
                    list_name.Add("Back");
                    var _selected_anime = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("[green]Select Anime Available[/]?")
                            .PageSize(10)
                            .MoreChoicesText("[grey](Move up and down to reveal more menu)[/]")
                            .AddChoices(list_name.ToArray()));

                    if (_selected_anime == "Back")
                    {
                        goto MenuPrompt;
                    }
                    foreach (var i in popular_list)
                    {
                        if (i.title.romaji != null)
                        {
                            if (_selected_anime == i.title.romaji)
                            {
                                new DetailAnime().GetDetailParams(i.id);
                            }
                        }
                        else if (i.title.english != null)
                        {
                            if (_selected_anime == i.title.english)
                            {
                                new DetailAnime().GetDetailParams(i.id);
                            }
                        }
                        else
                        {
                            if (_selected_anime == i.title.english)
                            {
                                new DetailAnime().GetDetailParams(i.id);
                            }
                        }
                    }
                } else if (_prompt == "Next Page")
                {
                    page++;
                    AnsiConsole.Clear();
                    PopularAnimeInvoke();
                } else if (_prompt == "Previous Page")
                {
                    page--;
                    AnsiConsole.Clear();
                    PopularAnimeInvoke();
                }
                else
                {
                    AnsiConsole.Clear();
                    Program.MenuHandlerInvoke();
                }
            }
        }
    }
}
