using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NontanCLI.API;
using NontanCLI.Feature.Search;
using NontanCLI.Feature;
using Spectre.Console;
using NontanCLI.Feature.Watch;
using NontanCLI.Feature.Popular;
using NontanCLI.Feature.Trending;
using NontanCLI.Feature.UpdateManager;
using NontanCLI.Feature.DownloadManager;
using System.Windows.Forms;
using System.Net.Http;
using System.Security.Policy;
using System.Diagnostics;
using System.Threading;
using System.Net;
using RestSharp;
using Newtonsoft.Json;
using NontanCLI.Models;
using Ionic.Zip;

namespace NontanCLI
{
    public class Program
    {

        public static string version = "1.0.1 beta.3.8.23";
        public static string buildVersion = "2";

        public static UpdatesRoot response;

        [Obsolete]
        static void Main(string[] args)
        {




            
            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(),"vlc")+ "/vlc.exe"))
            {
                if (!AnsiConsole.Confirm("vlc is missing, you must have vlc to use this tool, download VLC ?"))
                {
                    Environment.Exit(0);
                } else
                {
                    try
                    {
                        var client = new RestClient("https://raw.githubusercontent.com/");
                        var request = new RestRequest("evnx32/NontanCLI/main/updates.json", Method.Get);

                        RestResponse req = client.Execute(request);

                        response = JsonConvert.DeserializeObject<UpdatesRoot>(req.Content);

                        if (response != null)
                        {
                            new DownloadManager().Download(response.whats_new[0].vlc_download_url);
                            using (ZipFile zip = ZipFile.Read(Directory.GetCurrentDirectory() + "/" + Path.GetFileName(response.whats_new[0].vlc_download_url)))
                            {
                                foreach (ZipEntry z in zip)
                                {
                                    z.Extract(Directory.GetCurrentDirectory(), ExtractExistingFileAction.OverwriteSilently);
                                }
                            }

                            // check if unzip is success
                            Thread.Sleep(3000);

                            if (File.Exists(Directory.GetCurrentDirectory() + "/vlc/vlc.exe"))
                            {
                                AnsiConsole.MarkupLine("[green]Downloaded and extracted successfully[/]");

                                // Delete File 

                                File.Delete(Directory.GetCurrentDirectory() + "/" + Path.GetFileName(response.whats_new[0].vlc_download_url));

                                Thread.Sleep(3000);
                                Console.Clear();
                            }
                            else
                            {
                                AnsiConsole.MarkupLine("[red]Downloaded successfully but failed to extract.[/]");
                                Thread.Sleep(5000);
                                Environment.Exit(0);
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                
            }
                

            UpdateManager.UpdateManagerInvoke();

            

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
                    AnsiConsole.MarkupLine($"[bold white]Version :[/] [bold green]{Program.version}[/]" + $" ({Program.buildVersion})\n\n");
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

            var _prompt = MenuHandler.MenuHandlerInvoke();

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
                    Console.Clear();

                    var _search_by = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]Select available menu[/]?")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Move up and down to reveal more menu)[/]")
                        .AddChoices("Search By Genres", "Search By Query","Back"));


                    
                    
                    switch (_search_by)
                    {
                        case "Search By Genres":
                            List<string> genres = new List<string>();

                            var _selected_genres = AnsiConsole.Prompt(
                                new MultiSelectionPrompt<string>()
                                    .Title("Select [green]Available Genres[/]?")
                                    .NotRequired() // Not required to have a favorite fruit
                                    .PageSize(10)
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
