using Newtonsoft.Json;
using NontanCLI.API;
using NontanCLI.Models;
using NontanCLI.Feature.Detail;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using RestSharp;


namespace NontanCLI.Feature.Search
{
    public class SearchAnime
    {

        private RestResponse req;
        private SearchRoot response;
        private AdvanceRoot AdvanceResponse;
        [Obsolete]
        public void SearchAnimeInvoke(string query)
        {
            Table _table = new Table();
            _table.Border = TableBorder.Ascii2;

            try
            {
                req = RestSharpHelper.GetResponse($"/meta/anilist/{query}");
                response = JsonConvert.DeserializeObject<SearchRoot>(req.Content);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



            Regex regex = new Regex(@"[\[\]]");

            _table.Title = new TableTitle($"\n\nSearch Result for [green]{query}[/]");
            _table.AddColumn("[green]ID[/]");
            _table.AddColumn("[green]Title[/]");
            _table.AddColumn("[green]Status[/]");
            _table.AddColumn("[green]Type[/]");
            _table.AddColumn("[green]Rating[/]");

            List<string> _list_name = new List<string>();
            List<SearchResultModel> _list_result = new List<SearchResultModel>();
            // Add some rows
            foreach (var item in response.results)
            {
                string _id = "";
                string _title = "";
                string _status = "";
                string _type = "";
                string _rating = "";
                _list_result.Add(item);


                if (item.id != null)
                {
                    _id = item.id.ToString();
                }
                if (item.title.romaji != null)
                {
                    _title = regex.Replace(item.title.romaji.ToString(),string.Empty);
                    _list_name.Add(regex.Replace(item.title.romaji.ToString(), string.Empty));

                }
                else if (item.title.english != null)
                {
                    _title = regex.Replace(item.title.english.ToString(), string.Empty);
                    _list_name.Add(regex.Replace(item.title.english.ToString(), string.Empty));

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
            _list_name.Add("Back");
            var _prompt = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[green]Select Anime Available[/]?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more menu)[/]")
                .AddChoices(_list_name.ToArray()));


            if (_prompt == "Back")
            {
                AnsiConsole.Clear();
                Program.MenuHandlerInvoke();
            }
            else
            {
                Console.WriteLine("------------");

                Console.WriteLine(_prompt);

                foreach (var i in _list_result)
                {
                    if (i.title.romaji != null)
                    {
                        if (_prompt == regex.Replace(i.title.romaji.ToString(), string.Empty))
                        {
                            new DetailAnime().GetDetailParams(i.id);
                        }
                    }
                    else if (i.title.english != null)
                    {
                        if (_prompt == regex.Replace(i.title.english.ToString(), string.Empty))
                        {
                            new DetailAnime().GetDetailParams(i.id);
                        }
                    }
                    else
                    {
                        if (_prompt == regex.Replace(i.title.english.ToString(), string.Empty))
                        {
                            new DetailAnime().GetDetailParams(i.id);
                        }
                    }
                }
            }
        }



        [Obsolete]
        public void AdvanceSearchByGenresInvoke(List<string> genres)
        {
            Table _table = new Table();

            Regex regex = new Regex(@"[\[\]]");

            try
            {
                string genresString = string.Join(",", genres.Select(g => $"\"{g}\"")); // Convert the genres list to a comma-separated string of quoted values
                req = RestSharpHelper.GetResponse($"/meta/anilist/advanced-search?genres=[{genresString}]");
                AdvanceResponse = JsonConvert.DeserializeObject<AdvanceRoot>(req.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            List<string> _list_name = new List<string>();
            // Add some rows

            if (AdvanceResponse.results != null)
            {
                var _selected_genres = string.Join(", ", genres);
                _table.Title = new TableTitle($"\n\nSearch Result By Genres [green]{_selected_genres}[/]");
                _table.AddColumn("[green]ID[/]");
                _table.AddColumn("[green]Title[/]");
                _table.AddColumn("[green]Status[/]");
                _table.AddColumn("[green]Type[/]");
                _table.AddColumn("[green]Rating[/]");
                List<AdvanceResultModel> _advance_list = new List<AdvanceResultModel>();
                foreach (var item in AdvanceResponse.results)
                {
                    _advance_list.Add(item);

                    string _id = "";
                    string _title = "";
                    string _status = "";
                    string _type = "";
                    string _rating = "";
                    _advance_list.Add(item);
                    if (item.id != null)
                    {
                        _id = item.id.ToString();
                    }
                    if (item.title.romaji != null)
                    {

                        _title = regex.Replace(item.title.romaji.ToString(), string.Empty);
                        _list_name.Add(regex.Replace(item.title.romaji.ToString(), string.Empty));

                    }
                    else if (item.title.english != null)
                    {
                        _title = regex.Replace(item.title.english.ToString(), string.Empty);
                        _list_name.Add(regex.Replace(item.title.english.ToString(), string.Empty));

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

                _list_name.Add("Back");
                var _prompt = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]Select Anime Available[/]?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more menu)[/]")
                    .AddChoices(_list_name.ToArray()));


                if (_prompt == "Back")
                {
                    AnsiConsole.Clear();
                    Program.MenuHandlerInvoke();
                }
                else
                {
                    foreach (var i in _advance_list)
                    {
                        if (i.title.romaji != null)
                        {
                            if (_prompt == regex.Replace(i.title.romaji.ToString(), string.Empty))
                            {
                                new DetailAnime().GetDetailParams(i.id);
                            }
                        }
                        else if (i.title.english != null)
                        {
                            if (_prompt == regex.Replace(i.title.english.ToString(), string.Empty))
                            {
                                new DetailAnime().GetDetailParams(i.id);
                            }
                        }
                        else
                        {
                            if (_prompt == regex.Replace(i.title.english.ToString(), string.Empty))
                            {
                                new DetailAnime().GetDetailParams(i.id);
                            }
                        }
                    }
                }
            } else
            {
                AnsiConsole.MarkupLine("[red]No result found[/]");
                // wait for 2 seconds
                Thread.Sleep(2000);
                // clear the screen
                AnsiConsole.Clear();
                Program.MenuHandlerInvoke();
            }
        }
    }
}
