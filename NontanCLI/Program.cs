using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NontanCLI.API;
using NontanCLI.Feature.Search;
using NontanCLI.Feature;
using Spectre.Console;
using NontanCLI.Feature.Watch;
using NontanCLI.Feature.Popular;
using NontanCLI.Feature.Trending;

namespace NontanCLI
{
    public class Program
    {

        public static string version = "1.0.0";

        [Obsolete]
        static void Main(string[] args)
        {

            // args -s search
            // args -w watch

            if (args.Length > 0)
            {
                if (args[0] == "-s")
                {
                    SearchAnime.SearchAnimeInvoke(args[1]);
                }

                if (args[0] == "-h")
                {
                    Console.WriteLine("Help");
                    Console.WriteLine("-s search anime, example -s Naruto");
                    Console.WriteLine("-w watch anime, example -w kubo-san-wa-mob-wo-yurusanai-episode-6");

                }

                if (args[0] == "-v")
                {
                    Console.WriteLine("Version : " + version);
                }

                if (args[0] == "-w")
                {
                    WatchAnime.WatchAnimeInvoke(args[1]);
                }
            }
            else
            {
                MenuHandlerInvoke();
            }

        }

        [Obsolete]
        public static void MenuHandlerInvoke()
        {

            var _prompt = Menu.MenuHandler();

            switch (_prompt)
            {
                case "Popular":
                    Console.Clear();
                    PopularAnime.PopularAnimeInvoke();
                    Console.ReadLine();
                    break;
                case "Trending":
                    Console.Clear();
                    TrendingAnime.TrendingAnimeInvoke();
                    Console.ReadLine();
                    break;
                case "Search":
                    var _search_by = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]Select Anime Available[/]?")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Move up and down to reveal more menu)[/]")
                        .AddChoices("Search By Genres", "Search By Query","Back"));


                    
                    
                    switch (_search_by)
                    {
                        case "Search By Genres":
                            List<string> genres = new List<string>();

                            var _selected_genres = AnsiConsole.Prompt(
                                new MultiSelectionPrompt<string>()
                                    .Title("What are your [green]favorite fruits[/]?")
                                    .NotRequired() // Not required to have a favorite fruit
                                    .PageSize(10)
                                    .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
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

                            // Write the selected fruits to the terminal
                            for (int i = 0; i < _selected_genres.Count; i++)
                            {
                                genres.Add(_selected_genres[i]);
                            }
                            
                            AnsiConsole.MarkupLine("You selected: [green]{0}[/]", string.Join(", ", genres));
                            SearchAnime.AdvanceSearchByGenresInvoke(genres);
                            break;

                        case "Search By Query":
                            var query = AnsiConsole.Ask<string>("Okay, what anime do you want to search ? [green]example : One Piece[/] > ");
                            SearchAnime.SearchAnimeInvoke(query);
                            break;

                        case "Back":
                            Console.Clear();
                            MenuHandlerInvoke();
                            break;
                    }


                    Console.ReadLine();
                    break;
                case "Bookmark":
                    Console.Clear();
                    Console.WriteLine("Bookmark");
                    Console.ReadLine();
                    break;
                case "Exit":
                    exit();
                    Console.Clear();
                    Console.WriteLine("Exit");
                    Console.ReadLine();
                    break;
            }
        }

        public static void exit()
        {
            Environment.Exit(0);
        }
    }
}
