using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Threading;
using Spectre.Console;

namespace NontanCLI.API
{
    public static class RestSharpHelper 
    {
        const string BaseUrl = "https://api.consumet.org/";

        // create a new instance of the RestSharp client
        private static RestClient client = new RestClient(BaseUrl);

        // create a new method to get the response
        public static RestResponse GetResponse(string endpoint)
        {
            RestResponse response = null;
            AnsiConsole.Status()
                .Start("Fetching Server...", ctx =>
                {
                    // Simulate some work
                    Thread.Sleep(1000);

                    // Update the status and spinner
                    ctx.Status("Retrieving Data From Server...");
                    ctx.Spinner(Spinner.Known.Star);
                    ctx.SpinnerStyle(Style.Parse("green"));

                    // set the endpoint
                    var request = new RestRequest(endpoint, Method.Get);

                    // execute the request
                    response = (RestResponse)client.Execute(request);

                    // log the response status code and content
                    ctx.Status($"Response status code: {response.StatusCode}");

                    // Simulate some work
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        ctx.Status("Data Retrieved Successfully!");
                    }
                    else
                    {
                        ctx.Status("Data Retrieval Failed!");
                    } 
                });

            // check the response status code
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response;
            }
            else
            {
                if (response.ErrorMessage == null)
                {
                    Console.WriteLine($"Error fetching data. Status code: {response.StatusCode}");

                    // Log Url and Status Code
                    Console.WriteLine($"Url: {response.ResponseUri}");
                    Console.WriteLine($"Status Code: {response.StatusCode}");
                    throw new Exception($"Error executing request. Status code: {response.StatusCode}");
                    
                }
                else
                {
                    Console.WriteLine($"Error fetching data. Error message: {response.ErrorMessage}");

                    // Log Url and Status Code
                    Console.WriteLine($"Url: {response.ResponseUri}");
                    Console.WriteLine($"Status Code: {response.StatusCode}");
                    throw new Exception($"Error executing request: {response.ErrorMessage}", response.ErrorException);
                }
            }
        }
    }
}
