using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Security.Policy;
using System.IO;
using Spectre.Console;
using System.Diagnostics;
using System.Runtime.InteropServices;
using AltoHttp;
using System.Windows.Forms;

namespace NontanCLI.Feature.DownloadManager
{
    internal class DownloadManager
    {

        HttpDownloader httpDownloader;


        public async Task DownloadManagerInvoke(string url, string filePath)
        {
            httpDownloader = new HttpDownloader(url, filePath);
            httpDownloader.DownloadCompleted += HttpDownloader_DownloadCompleted;
            httpDownloader.ProgressChanged += HttpDownloader_ProgressChanged;
            httpDownloader.Start();
        }

        private void HttpDownloader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            AnsiConsole.Progress()
                .AutoClear(false)
                .Columns(new ProgressColumn[]
                {
                    new TaskDescriptionColumn(),
                    new ProgressBarColumn(),
                    new PercentageColumn(),
                    new RemainingTimeColumn()
                })
                .Start(ctx =>
                {
                    var task = ctx.AddTask("Downloading...");
                    task.Increment((double)e.Progress);
                });
        }

        private void HttpDownloader_DownloadCompleted(object sender, EventArgs e)
        {
            Console.WriteLine("Download Complete");
        }
    }
}
