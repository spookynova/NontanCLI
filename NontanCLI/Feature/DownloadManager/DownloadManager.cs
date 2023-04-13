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
using System.ComponentModel;
using System.Net;
using System.Threading;

namespace NontanCLI.Feature.DownloadManager
{
    class DownloadManager
    {
        public void Download(string remoteUri)
        {
            string FilePath = Directory.GetCurrentDirectory() + "/" + Path.GetFileName(remoteUri); // path where download file to be saved, with filename, here I have taken file name from supplied remote url
            using (WebClient client = new WebClient())
            {
                try
                {
                    Uri uri = new Uri(remoteUri);
                    
                    //delegate method, which will be called after file download has been complete.
                    client.DownloadFileCompleted += new AsyncCompletedEventHandler(Extract);
                    //delegate method for progress notification handler.
                    client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgessChanged);
                    // uri is the remote url where filed needs to be downloaded, and FilePath is the location where file to be saved
                    client.DownloadFileAsync(uri, FilePath);

                    while (client.IsBusy)
                    {
                        Thread.Sleep(1000);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public void Extract(object sender, AsyncCompletedEventArgs e)
        {
            Console.WriteLine("File has been downloaded.");
        }
        public void ProgessChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            int totalSize = (int)e.TotalBytesToReceive;
            int downloadedSize = (int)e.BytesReceived;
            int percentage = (int)(downloadedSize * 100.0 / totalSize);
            int charsToFill = (int)(percentage / 2.0); // each character represents 2% progress
            Console.Write($"\rDownloading... [{new string('#', charsToFill)}{new string(' ', 50 - charsToFill)}] {percentage}%");
        }
    }
}
