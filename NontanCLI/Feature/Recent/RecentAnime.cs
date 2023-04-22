using Newtonsoft.Json;
using NontanCLI.API;
using NontanCLI.Feature.Detail;
using NontanCLI.Models;
using RestSharp;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NontanCLI.Feature.Recent
{
    public class RecentAnime
    {
        private int page = 1;

        private RestResponse req;
        private RecentRoot response;

        [Obsolete]
        public void RecentAnimeInvoke()
        {
            Table table = new Table();
            Regex regex = new Regex(@"[\[\]]");


            try
            {
                req = RestSharpHelper.GetResponse($"meta/anilist/recent-episodes?page={page}");
                response = JsonConvert.DeserializeObject<RecentRoot>(req.Content);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            if (response != null)
            {

                table.Title = new TableTitle($"\n\n[green]Recent Anime Page {response.currentPage} / {response.totalPages} Result : {response.totalResults}[/]");
                table.AddColumn("[green]ID[/]");
                table.AddColumn("[green]Title[/]");
                table.AddColumn("[green]Episode[/]");
                table.AddColumn("[green]Episode Title[/]");
                table.AddColumn("[green]Type[/]");
                table.AddColumn("[green]Rating[/]");
                List<string> list_name = new List<string>();
                List<RecentResultModel> recent_list = new List<RecentResultModel>();
                foreach (var item in response.results)
                {
                    recent_list.Add(item);

                    string id = "";
                    string title = "";
                    string type = "";
                    string rating = "";
                    string episodeNumber = "";
                    string episodeTitle = "";
                    if (item.id != null)
                    {
                        id = item.id.ToString();
                    }
                    if (item.title.romaji != null)
                    {

                        title = regex.Replace(item.title.romaji.ToString(), string.Empty);
                        list_name.Add(regex.Replace(item.title.romaji.ToString(), string.Empty));

                    }
                    else if (item.title.english != null)
                    {
                        title = regex.Replace(item.title.english.ToString(), string.Empty);
                        list_name.Add(regex.Replace(item.title.english.ToString(), string.Empty));

                    }
                    if (item.episodeNumber != null)
                    {
                        episodeNumber = item.episodeNumber.ToString();
                    }

                    if (item.episodeTitle != null)
                    {
                        episodeTitle = item.episodeTitle.ToString();
                    }
                    if (item.type != null)
                    {
                        type = item.type.ToString();
                    }
                    if (item.rating != null)
                    {
                        rating = item.rating.ToString();
                    }


                    table.AddRow(id, title,  episodeNumber ,episodeTitle, type, rating);

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
                }
                else
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
                    foreach (var i in recent_list)
                    {
                        if (i.title.romaji != null)
                        {
                            if (_selected_anime == regex.Replace(i.title.romaji.ToString(), string.Empty))
                            {
                                new DetailAnime().GetDetailParams(i.id);
                            }
                        }
                        else if (i.title.english != null)
                        {
                            if (_selected_anime == regex.Replace(i.title.english.ToString(), string.Empty))
                            {
                                new DetailAnime().GetDetailParams(i.id);
                            }
                        }
                        else
                        {
                            if (_selected_anime == regex.Replace(i.title.english.ToString(), string.Empty))
                            {
                                new DetailAnime().GetDetailParams(i.id);
                            }
                        }
                    }
                }
                else if (_prompt == "Next Page")
                {
                    page++;
                    AnsiConsole.Clear();
                    RecentAnimeInvoke();
                }
                else if (_prompt == "Previous Page")
                {
                    page--;
                    AnsiConsole.Clear();
                    RecentAnimeInvoke();
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
