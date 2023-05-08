using NontanCLI.Models;
using RestSharp;
using Spectre.Console;
using NontanCLI.API;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using NontanCLI.Feature.Detail;

namespace NontanCLI.Feature.Airing_Schedule
{
    public class AiringScheduleAnime
    {
        private int page = 1;

        private RestResponse req;
        private AiringScheduleRoot response;


        public void AiringScheduleAnimeInvoke()
        {
            Table table = new Table();
            table.Border = TableBorder.Ascii2;
            Regex regex = new Regex(@"[\[\]]");

            try
            {
                req = RestSharpHelper.GetResponse($"meta/anilist/airing-schedule?page={page}");
                response = JsonConvert.DeserializeObject<AiringScheduleRoot>(req.Content);
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
                return;
            }

            if (response != null)
            {
                table.Title = new TableTitle($"\n\n[green]Airing Schedule Page {response.currentPage}[/]");

                table.AddColumn("[green]ID[/]");
                table.AddColumn("[green]Title[/]");
                table.AddColumn("[green]Episode[/]");
                table.AddColumn("[green]Airing At[/]");
                table.AddColumn("[green]Type[/]");
                table.AddColumn("[green]Country[/]");
                table.AddColumn("[green]Rating[/]");

                List<string> list_name = new List<string>();
                List<AiringScheduleResultModel> _airing_schedule_list = new List<AiringScheduleResultModel>();

                foreach (var item in response.results)
                {

                    _airing_schedule_list.Add(item);

                    string _id = "";
                    string _title = "";
                    string _type = "";
                    string _rating = "";
                    string _episode = "";
                    string _airing_at = "";
                    string _country = "";

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
                    if (item.episode != null)
                    {
                        _episode = item.episode.ToString();
                    }

                    if (item.airingAt != null)
                    {
                        _airing_at = item.airingAt.ToString();
                        // First, convert the Unix timestamp to a DateTime object
                        DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                        DateTime _timestamp = _unixEpoch.AddSeconds(double.Parse(_airing_at));

                        // Then, format the DateTime object as a string in the desired format
                        string formattedDate = _timestamp.ToString("MMMM dd, yyyy, 'at' hh:mm:ss tt 'UTC'");

                        _airing_at = formattedDate;
                    }
                    if (item.type != null)
                    {
                        _type = item.type.ToString();
                    }
                    if (item.rating != null)
                    {
                        _rating = item.rating.ToString();
                    }

                    if (item.country != null)
                    {
                        _country = item.country.ToString();
                    }


                    table.AddRow(_id, _title, _episode, _airing_at, _type, _country, _rating);

                }
                AnsiConsole.Render(table);

                list_name.Add("Back");
                var _selected_anime = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]Select Anime for more information[/]?")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Move up and down to reveal more menu)[/]")
                        .AddChoices(list_name.ToArray()));

                if (_selected_anime == "Back")
                {
                    Program.MenuHandlerInvoke();
                } else
                {
                    foreach (var i in _airing_schedule_list)
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
            }
            else
            {

                AnsiConsole.MarkupLine("[red]No result found[/]");
                Thread.Sleep(2000);
                Console.Clear();
                Program.MenuHandlerInvoke();
            }
        }
    }
}
