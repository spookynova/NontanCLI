using NontanCLI.API;
using Spectre.Console;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NontanCLI.Models;
using NontanCLI.Feature.DownloadManager;
using RestSharp;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Net;
using System.Text;
using NontanCLI.Utils;
using System.Threading.Tasks.Sources;
using SevenZipExtractor;

namespace NontanCLI.Feature.Watch
{

    public class WatchAnime
    {

        private RestResponse? req;
        private static WatchRoot? response;


        private static string? vtt_url;


        [Obsolete]
        public void WatchAnimeInvoke(string episode_id)
        {
            try
            {
                string Query = "";
                if (Constant.provider == "zoro")
                {
                    Query = $"anime/zoro/watch?episodeId={episode_id}";
                } else
                {
                    Query = $"anime/{Constant.provider}/watch/{episode_id}";
                }

                req = RestSharpHelper.GetResponse(Query);
                response = JsonConvert.DeserializeObject<WatchRoot>(req.Content!);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            // foreach quality

            List<string> quality_selection = new List<string>();


            if (response.sources.Count == 0)
            {

                AnsiConsole.MarkupLine("[red]Something wrong, i can feet it [/]");
                AnsiConsole.MarkupLine(response.message.ToString());
                Thread.Sleep(5000);
                AnsiConsole.Clear();
                Program.MenuHandlerInvoke();
                return;
            }

            foreach (var item in response.sources!)
            {
                quality_selection.Add(item.quality!.ToString());   
            }

            quality_selection.Add("Back");

            var _prompt = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\n[green]Select Quality Anime Video[/]?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more menu)[/]")
                    .AddChoices(quality_selection.ToArray()));

            if (_prompt == "Back")
            {
                new MenuHandler().MenuHandlerInvoke();
            }
            else
            {

                for (int i = 0; i < response.sources.Count; i++)
                {
                    if (response.sources[i].quality!.ToString() == _prompt)
                    {
                        var _selected_player = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("\n[green]Select video player available[/]?")
                                .PageSize(10)
                                .MoreChoicesText("[grey](Move up and down to reveal more menu)[/]")
                                .AddChoices(new[] { "VLC", "Browser" }));

                        if (_selected_player == "VLC")
                        {
                            PlayOnVLC(response.sources[i].url!.ToString());
                        }
                        else if (_selected_player == "Browser")
                        {
                            if (response.subtitles != null)
                            {
                                List<string> subtitles = new List<string>();
                                foreach (var item in response.subtitles)
                                {
                                    subtitles.Add(item.lang!.ToString());
                                }
                                var _selectted_subtitles = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                        .Title("\n[green]Select Subtitle available[/]?")
                                        .PageSize(10)
                                        .MoreChoicesText("[grey](Move up and down to reveal more menu)[/]")
                                        .AddChoices(subtitles.ToArray()));

                                foreach (var sub in response.subtitles)
                                {
                                    if (_selectted_subtitles == sub.lang!.ToString())
                                    {
                                        vtt_url = sub.url!.ToString();
                                    }
                                }
                            } else
                            {
                                vtt_url = "";
                            }
                            M3U8Helper.setMedia(response.sources[i].url!.ToString(), vtt_url!);
                            M3U8Helper.Start();
                        }
                        else
                        {
                            M3U8Helper.setMedia(response.sources[i].url!.ToString(), vtt_url!);
                            M3U8Helper.Start();
                        }
                    }
                }
            }
        }

        [Obsolete]
        private void PlayOnVLC(string url)
        {


            VLC:

            string CURRENT_DIR = AppDomain.CurrentDomain.BaseDirectory;

            if (!File.Exists(CURRENT_DIR + "\\vlc\\vlc.exe"))
            {
                AnsiConsole.MarkupLine("[bold red]Cannot play with VLC, VLC is missing !![/]");
                UpdatesRoot response;
                if (!AnsiConsole.Confirm("vlc is missing, do you want to download it?"))
                {
                    Console.Clear();
                    Program.MenuHandlerInvoke();
                    return;
                }
                else
                {
                    try
                    {
                        var client = new RestClient("https://raw.githubusercontent.com/");
                        var request = new RestRequest("evnx32/NontanCLI/main/updates.json", Method.Get);

                        RestResponse req = client.Execute(request);

                        response = JsonConvert.DeserializeObject<UpdatesRoot>(req.Content!)!;

                        if (response != null)
                        {

                            new DownloadManager.DownloadManager().Download(response.whats_new[0].vlc_download_url);
                            using (ArchiveFile archiveFile = new ArchiveFile(Directory.GetCurrentDirectory() + "/" + Path.GetFileName(response.whats_new[0].vlc_download_url)))
                            {
                                archiveFile.Extract(Directory.GetCurrentDirectory());
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
                                goto VLC;
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

                Console.ReadKey();
                return;
            }


            //Process.Start(CURRENT_DIR + "\\vlc\\vlc.exe", url);

            Process vlcProcess = new Process();
            vlcProcess.StartInfo.FileName = CURRENT_DIR + "\\vlc\\vlc.exe";
            vlcProcess.StartInfo.Arguments = $"{url} --sub-file={vtt_url}";
            vlcProcess.Start();

            Thread.Sleep(5000);
            while (true)
            {
                if (Process.GetProcessesByName("vlc").Length == 0)
                {
                    Console.Clear();
                    Program.MenuHandlerInvoke();
                    break;
                }
            }
        }
    }
}
