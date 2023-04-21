using NontanCLI.API;
using Spectre.Console;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NontanCLI.Models;
using RestSharp;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Linq;
using System.Text;
using System.Windows;
using WebMediaToolkit.Hls;
using System.Web;
using NontanCLI.Utils;

namespace NontanCLI.Feature.Watch
{
    public class WatchAnime
    {

        private RestResponse req;
        private static WatchRoot response;


        private static string m3u8_url;


        private static string htmlFilePath = @"extension/index.html";
        private static string baseAddress = "http://localhost:8000/";

        [Obsolete]
        public void WatchAnimeInvoke(string episode_id)
        {

            try
            {
                req = RestSharpHelper.GetResponse($"meta/anilist/watch/{episode_id}");
                response = JsonConvert.DeserializeObject<WatchRoot>(req.Content);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            // foreach quality

            List<string> quality_selection = new List<string>();


            foreach (var item in response.sources)
            {
                quality_selection.Add(item.quality.ToString());   
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
                while (true)
                {

                    foreach (var item in response.sources)
                    {
                        if (item.quality.ToString() == _prompt)
                        {

                            var _selected_player = AnsiConsole.Prompt(
                                new SelectionPrompt<string>()
                                    .Title("\n[green]Select video player available[/]?")
                                    .PageSize(10)
                                    .MoreChoicesText("[grey](Move up and down to reveal more menu)[/]")
                                    .AddChoices(new[] {"VLC" , "Browser (localhost)"} ));

                            if (_selected_player == "VLC")
                            {
                                PlayOnVLC(item.url.ToString());
                            } else if (_selected_player == "Browser")
                            {
                                PlayOnBrowser(item.url.ToString());
                            } else
                            {
                                PlayOnBrowser(item.url.ToString());
                            }

                        }
                    }
                }
            }
        }

        [Obsolete]
        private void PlayOnVLC(string url)
        {
            string CURRENT_DIR = AppDomain.CurrentDomain.BaseDirectory;

            Process.Start(CURRENT_DIR + "\\vlc\\vlc.exe", url);


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

        private void PlayOnBrowser(string url)
        {
            // Bypass CORS
            m3u8_url = Constant.CORS + url;
            Thread serverThread = new Thread(() =>
            {
                HttpListener listener = new HttpListener();
                listener.Prefixes.Add(baseAddress);
                listener.Start();
                Console.WriteLine($"Server started at {baseAddress}. Listening for requests...");

                while (true)
                {
                    HttpListenerContext ctx = listener.GetContext();
                    HttpListenerRequest request = ctx.Request;
                    HttpListenerResponse resp = ctx.Response;

                    string requestUrl = request.Url.AbsolutePath;

                    if (requestUrl == "/")
                    {
                        // Serve the HTML file
                        string html = File.ReadAllText(htmlFilePath);
                        byte[] buffer = Encoding.UTF8.GetBytes(html);
                        resp.ContentType = "text/html";
                        resp.ContentLength64 = buffer.Length;
                        resp.OutputStream.Write(buffer, 0, buffer.Length);
                        resp.OutputStream.Close();
                    }
                    else if (requestUrl == "/hls/animevideo.m3u8")
                    {
                        // Redirect to the online m3u8 URL
                        resp.Redirect(m3u8_url);
                        resp.OutputStream.Close();
                    }
                    else
                    {
                        // Return 404 for any other requests cuz i really don't care lol 
                        resp.StatusCode = 404;
                        resp.OutputStream.Close();
                    }
                }
            });

            serverThread.Start();

            // Open the HTML file in the default web browser
            Process.Start(baseAddress);

            // Wait for the server thread to exit
            serverThread.Join();
        }

    }
}
