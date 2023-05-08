// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using NontanCLI.Feature;
using NontanCLI.Feature.DownloadManager;
using NontanCLI.Feature.Popular;
using NontanCLI.Feature.Recent;
using NontanCLI.Feature.Search;
using NontanCLI.Feature.Trending;
using NontanCLI.Feature.UpdateManager;
using NontanCLI.Feature.Watch;
using NontanCLI.Feature.Airing_Schedule;
using NontanCLI.Models;
using NontanCLI.Utils;
using RestSharp;
using Spectre.Console;
using SevenZipExtractor;
namespace NontanCLI
{
    public class Program
    {

        public static string version = "1.0.4 beta.08.5.23";
        public static string buildVersion = "5";


        [Obsolete]
        static void Main(string[] args)
        {

            // set title
            Console.Title = "NontanCLI";
            
            new UpdateManager().UpdateManagerInvoke();

            if (args.Length > 0)
            {
                if (args[0] == "-h" || args[0] == "-help" || args[0] == "--help")
                {
                    Console.WriteLine("-- Help --");
                    Console.WriteLine("-s search anime, example -s Naruto");
                    Console.WriteLine("-w watch anime, example -w kubo-san-wa-mob-wo-yurusanai-episode-6");
                    Console.WriteLine("-p popular anime");
                    Console.WriteLine("-t trending anime");
                    Console.WriteLine("-r recent anime");
                    Console.WriteLine("-a airing schedule");
                    Console.WriteLine("-v version");
                    Console.WriteLine("-h help");

                }

                if (args[0] == "-s" || args[0] == "-search")
                {
                    if (args.Length < 2)
                    {
                        AnsiConsole.MarkupLine("[red]Error : Please enter anime name[/]");
                        return;
                    }

                    Constant.InitConfig();
                    new SearchAnime().SearchAnimeInvoke(args[1]);
                }

                if (args[0] == "-v" || args[0] == "-version")
                {
                    Constant.InitConfig();
                    AnsiConsole.MarkupLine($"[bold white]Version :[/] [bold green]{version}[/]" + $" ({buildVersion})\n\n");
                }

                if (args[0] == "-t" || args[0] == "-trending")
                {

                    Constant.InitConfig();
                    new TrendingAnime().TrendingAnimeInvoke();
                }

                if (args[0] == "-a" || args[0] == "-airing-schedule")
                {
                    Constant.InitConfig();
                    new AiringScheduleAnime().AiringScheduleAnimeInvoke();
                }

                if (args[0] == "-r" || args[0] == "-recent")
                {
                    Constant.InitConfig();
                    new RecentAnime().RecentAnimeInvoke();
                }

                if (args[0] == "-p" || args[0] == "-popular")
                {
                    Constant.InitConfig();
                    new PopularAnime().PopularAnimeInvoke();
                }

                if (args[0] == "-w" || args[0] == "-watch")
                {


                    if (args.Length < 2)
                    {
                        AnsiConsole.MarkupLine("[red]Error : Please enter episode id[/]");
                        return;
                    }

                    Constant.InitConfig();
                    new WatchAnime().WatchAnimeInvoke(args[1]);
                }
            }
            else
            {
                Constant.InitConfig();
                MenuHandlerInvoke();
            }
        }

        [Obsolete]
        public static void MenuHandlerInvoke()
        {

            var _prompt = new MenuHandler().MenuHandlerInvoke();

            switch (_prompt)
            {
                case "Popular Anime":
                    Console.Clear();
                    new PopularAnime().PopularAnimeInvoke();
                    Console.ReadLine();
                    break;
                case "Trending Anime":
                    Console.Clear();
                    new TrendingAnime().TrendingAnimeInvoke();
                    Console.ReadLine();
                    break;
                case "Recent Anime":
                    Console.Clear();
                    new RecentAnime().RecentAnimeInvoke();
                    Console.ReadLine();
                    break;
                case "Search Anime":
                    Console.Clear();

                    var _search_by = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]Select available menu[/]?")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Move up and down to reveal more menu)[/]")
                        .AddChoices("Search By Genres", "Search By Query", "Back"));
                        
                    switch (_search_by)
                    {
                        case "Search By Genres":

                        Genres:
                            List<string> genres = new List<string>();
                            var _selected_genres = AnsiConsole.Prompt(
                                new MultiSelectionPrompt<string>()
                                    .Title("Select [green]Available Genres[/]?")
                                    .PageSize(10)
                                    .Required()
                                    .MoreChoicesText("[grey](Move up and down to reveal more Genres)[/]")
                                    .InstructionsText(
                                        "[grey](Press [blue]<space>[/] to toggle a genres, " +
                                        "[green]<enter>[/] to accept genres)[/]")
                                    .AddChoices(new[] {
                                        "Action", "Adventure", "Cars",
                                        "Comedy", "Drama", "Fantasy",
                                        "Horror", "Mahou Shoujo", "Music",
                                        "Mystery", "Psychological", "Romance",
                                        "Sci-Fi", "Slice of Life", "Sports",
                                        "Supernatural", "Thriller"
                                    }));

                            if (_selected_genres.Count == 0)
                            {
                                AnsiConsole.MarkupLine("[red]You must select at least one genres, Press anyting to continue[/]");
                                Console.ReadLine();
                                Console.Clear();
                                goto Genres;
                            }

                            // Write the selected fruits to the terminal
                            for (int i = 0; i < _selected_genres.Count; i++)
                            {
                                genres.Add(_selected_genres[i]);
                            }

                            AnsiConsole.MarkupLine("You selected: [green]{0}[/]", string.Join(", ", genres));
                            new SearchAnime().AdvanceSearchByGenresInvoke(genres);
                            break;

                        case "Search By Query":
                            var query = AnsiConsole.Ask<string>("Okay, what anime do you want to search ? [green]example : One Piece[/] > ");
                            new SearchAnime().SearchAnimeInvoke(query);
                            break;

                        case "Back":
                            Console.Clear();
                            MenuHandlerInvoke();
                            break;

                        default:
                            Console.Clear();
                            MenuHandlerInvoke();
                            break;
                    }


                    Console.ReadLine();
                    break;

                case "Airing Schedule":
                    Console.Clear();
                    new AiringScheduleAnime().AiringScheduleAnimeInvoke();
                    
                    break;

                case "Exit":
                    exit();
                    break;

                default:
                    Console.Clear();
                    MenuHandlerInvoke();
                    break;
            }
        }

        public static void exit()
        {
            Environment.Exit(0);
        }
    }
}
