using LibVLCSharp.Shared;
using NontanCLI.Feature.Detail;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace NontanCLI.Feature
{
    internal class WatchDirect3D
    {
        [Obsolete]
        public static void MediaPlayerInvoke(string url, string id)
        {
            Core.Initialize();
            
            using (var _libvlc = new LibVLC())
            {
                using (var _mediaPlayer = new MediaPlayer(_libvlc))
                {
                    var _media = new Media(_libvlc, url, FromType.FromLocation);

                    _media.Parse(MediaParseOptions.ParseNetwork);
                    _mediaPlayer.Play(_media);
                    var volume = _mediaPlayer.Volume;
                    Console.WriteLine("Volume: " + volume);

                    while (true)
                    {
                        var key = Console.ReadKey(true);

                        switch (key.Key)
                        {
                            case ConsoleKey.P:
                                _mediaPlayer.Pause();
                                Console.WriteLine("Video Paused");
                                break;

                            case ConsoleKey.R:
                                _mediaPlayer.Play();
                                Console.WriteLine("Video Resumed");
                                break;

                            case ConsoleKey.S:
                                _mediaPlayer.Stop();
                                Console.WriteLine("Video Stopped");
                                break;

                            case ConsoleKey.V:
                                Console.WriteLine("Enter volume level (0-100):");
                                var input = Console.ReadLine();
                                int volumeLevel;
                                if (int.TryParse(input, out volumeLevel))
                                {
                                    _mediaPlayer.Volume = volumeLevel;
                                    Console.WriteLine("Volume set to " + volumeLevel);
                                }
                                break;

                            case ConsoleKey.LeftArrow:
                                var currentTime = _mediaPlayer.Time;
                                var newTime = currentTime -= 10000;
                                _mediaPlayer.Time = newTime;
                                Console.WriteLine("Duration: " + TimeSpan.FromMilliseconds(newTime).ToString(@"hh\:mm\:ss"));
                                break;

                            case ConsoleKey.RightArrow:
                                currentTime = _mediaPlayer.Time;
                                newTime = currentTime += 10000;
                                _mediaPlayer.Time = newTime;
                                Console.WriteLine("Duration: " + TimeSpan.FromMilliseconds(newTime).ToString(@"hh\:mm\:ss"));
                                break;

                            case ConsoleKey.UpArrow:
                                _mediaPlayer.Volume += 10;
                                Console.WriteLine("Volume : " + _mediaPlayer.Volume.ToString() + "%");
                                break;

                            case ConsoleKey.DownArrow:
                                _mediaPlayer.Volume -= 10;
                                Console.WriteLine("Volume : " + _mediaPlayer.Volume.ToString() + "%");
                                break;

                            case ConsoleKey.Q:
                                Console.WriteLine("Exiting...");
                                // exit _mediaplayer
                                return;


                        }
                    }
                }
            }
        }


        [Obsolete]
        public static void MediaPlayerInvoke(string url)
        {
            Core.Initialize();

            using (var _libvlc = new LibVLC())
            {
                using (var _mediaPlayer = new MediaPlayer(_libvlc))
                {
                    var _media = new Media(_libvlc, url, FromType.FromLocation);

                    _media.Parse(MediaParseOptions.ParseNetwork);
                    _mediaPlayer.Play(_media);
                    var volume = _mediaPlayer.Volume;
                    Console.WriteLine("Volume: " + volume);

                    while (true)
                    {
                        var key = Console.ReadKey(true);

                        switch (key.Key)
                        {
                            case ConsoleKey.P:
                                _mediaPlayer.Pause();
                                Console.WriteLine("Video Paused");
                                break;

                            case ConsoleKey.R:
                                _mediaPlayer.Play();
                                Console.WriteLine("Video Resumed");
                                break;

                            case ConsoleKey.S:
                                _mediaPlayer.Stop();
                                Console.WriteLine("Video Stopped");
                                break;

                            case ConsoleKey.V:
                                Console.WriteLine("Enter volume level (0-100):");
                                var input = Console.ReadLine();
                                int volumeLevel;
                                if (int.TryParse(input, out volumeLevel))
                                {
                                    _mediaPlayer.Volume = volumeLevel;
                                    Console.WriteLine("Volume set to " + volumeLevel);
                                }
                                break;

                            case ConsoleKey.LeftArrow:
                                var currentTime = _mediaPlayer.Time;
                                var newTime = currentTime -= 10000;
                                _mediaPlayer.Time = newTime;
                                Console.WriteLine("Duration: " + TimeSpan.FromMilliseconds(newTime).ToString(@"hh\:mm\:ss"));
                                break;

                            case ConsoleKey.RightArrow:
                                currentTime = _mediaPlayer.Time;
                                newTime = currentTime += 10000;
                                _mediaPlayer.Time = newTime;
                                Console.WriteLine("Duration: " + TimeSpan.FromMilliseconds(newTime).ToString(@"hh\:mm\:ss"));
                                break;

                            case ConsoleKey.UpArrow:
                                _mediaPlayer.Volume += 10;
                                Console.WriteLine("Volume : " + _mediaPlayer.Volume.ToString() + "%");
                                break;

                            case ConsoleKey.DownArrow:
                                _mediaPlayer.Volume -= 10;
                                Console.WriteLine("Volume : " + _mediaPlayer.Volume.ToString() + "%");
                                break;

                            case ConsoleKey.Q:
                                Console.WriteLine("Exiting...");
                                return;
                        }
                    }
                }
            }
        }
    }
}
