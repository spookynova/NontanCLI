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
            Table _table = new Table();
            _table.Border = TableBorder.Ascii2;

            Regex regex = new Regex(@"[\[\]]");

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

                _table.Title = new TableTitle($"\n\n[green]Popular Anime Page {response.currentPage}[/]");
                _table.AddColumn("[green]ID[/]");
                _table.AddColumn("[green]Title[/]");
                _table.AddColumn("[green]Status[/]");
                _table.AddColumn("[green]Type[/]");
                _table.AddColumn("[green]Rating[/]");
                List<string> list_name = new List<string>();
                List<PopularResultModel> popular_list = new List<PopularResultModel>();
                foreach (var item in response.results)
                {
                    popular_list.Add(item);

                    string _id = "";
                    string _title = "";
                    string _status = "";
                    string _type = "";
                    string _rating = "";
                    if (item.id != null)
                    {
                        _id = item.id.ToString();
                    }
                    if (item.title.romaji != null)
                    {

                        _title = regex.Replace(item.title.romaji.ToString(), string.Empty);
                        list_name.Add(regex.Replace(item.title.romaji.ToString(), string.Empty));

                    }
                    else if (item.title.english != null)
                    {
                        _title = regex.Replace(item.title.english.ToString(), string.Empty);
                        list_name.Add(regex.Replace(item.title.english.ToString(), string.Empty));

                    }
                    if (item.status != null)
                    {
                        _status = item.status.ToString();
                    }
                    if (item.type != null)
                    {
                        _type = item.type.ToString();
                    }
                    if (item.rating != null)
                    {
                        _rating = item.rating.ToString();
                    }


                    if (_status == "Completed")
                    {
                        _table.AddRow(_id, _title, "[green]" + _status + "[/]", _type, _rating);

                    }
                    else if (_status == "Ongoing")
                    {
                        _table.AddRow(_id, _title, "[yellow]" + _status + "[/]", _type, _rating);

                    }
                }

                AnsiConsole.Render(_table);

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
