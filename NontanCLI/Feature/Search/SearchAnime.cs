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

        public RestResponse req;
        public SearchRoot response;
        public AdvanceRoot AdvanceResponse;
        [Obsolete]
        public void SearchAnimeInvoke(string query)
        {
            Table table = new Table();

            try
            {
                req = RestSharpHelper.GetResponse($"/meta/anilist/{query}");
                response = JsonConvert.DeserializeObject<SearchRoot>(req.Content);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            table.Title = new TableTitle($"\n\nSearch Result for [green]{query}[/]");
            table.AddColumn("[green]ID[/]");
            table.AddColumn("[green]Title[/]");
            table.AddColumn("[green]Status[/]");
            table.AddColumn("[green]Type[/]");
            table.AddColumn("[green]Rating[/]");

            List<string> list_name = new List<string>();
            List<SearchResultModel> list_result = new List<SearchResultModel>();
            // Add some rows
            foreach (var item in response.results)
            {
                string id = "";
                string title = "";
                string status = "";
                string type = "";
                string rating = "";
                list_result.Add(item);


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
            list_name.Add("Back");
            var _prompt = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[green]Select Anime Available[/]?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more menu)[/]")
                .AddChoices(list_name.ToArray()));


            if (_prompt == "Back")
            {
                AnsiConsole.Clear();
                Program.MenuHandlerInvoke();
            }
            else
            {
                foreach (var i in list_result)
                {
                    if (i.title.romaji != null)
                    {
                        if (_prompt == i.title.romaji)
                        {
                            new DetailAnime().GetDetailParams(i.id);
                        }
                    }
                    else if (i.title.english != null)
                    {
                        if (_prompt == i.title.english)
                        {
                            new DetailAnime().GetDetailParams(i.id);
                        }
                    }
                    else
                    {
                        if (_prompt == i.title.english)
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
            Table table = new Table();


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

            List<string> list_name = new List<string>();
            List<AdvanceResultModel> list_result = new List<AdvanceResultModel>();
            // Add some rows

            if (AdvanceResponse.results != null)
            {
                var _selected_genres = string.Join(", ", genres);
                table.Title = new TableTitle($"\n\nSearch Result By Genres [green]{_selected_genres}[/]");
                table.AddColumn("[green]ID[/]");
                table.AddColumn("[green]Title[/]");
                table.AddColumn("[green]Status[/]");
                table.AddColumn("[green]Type[/]");
                table.AddColumn("[green]Rating[/]");
                List<AdvanceResultModel> popular_list = new List<AdvanceResultModel>();
                foreach (var item in AdvanceResponse.results)
                {
                    popular_list.Add(item);

                    string id = "";
                    string title = "";
                    string status = "";
                    string type = "";
                    string rating = "";
                    list_result.Add(item);
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

                list_name.Add("Back");
                var _prompt = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]Select Anime Available[/]?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more menu)[/]")
                    .AddChoices(list_name.ToArray()));


                if (_prompt == "Back")
                {
                    AnsiConsole.Clear();
                    Program.MenuHandlerInvoke();
                }
                else
                {
                    foreach (var i in list_result)
                    {
                        if (i.title.romaji != null)
                        {
                            if (_prompt == i.title.romaji)
                            {
                                new DetailAnime().GetDetailParams(i.id);
                            }
                        }
                        else if (i.title.english != null)
                        {
                            if (_prompt == i.title.english)
                            {
                                new DetailAnime().GetDetailParams(i.id);
                            }
                        }
                        else
                        {
                            if (_prompt == i.title.english)
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
