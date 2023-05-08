using AspNetCore.Proxy;
using Microsoft.AspNetCore.HttpOverrides;
using System.Diagnostics;
using NontanCLI.Utils;
using System.Net;
using NontanCLI;

public class M3U8Helper {

    const string myAllowSpecificOrigins = "corsPolicy";
    public static string m3u8_url = "";
    private static string vtt_url = ""; 

    public static void setMedia(string m3u8, string vtt)
    {
        m3u8_url = m3u8;
        vtt_url = vtt;
    }

    [Obsolete]
    public static void Start()
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddOutputCache(options =>
        {
            options.AddPolicy("m3u8", builder =>
            {
                builder.Cache();
                builder.Expire(TimeSpan.FromSeconds(5));
            });
        });
        builder.Services.AddControllers();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddProxies();
        var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
        if (!builder.Environment.IsDevelopment()) builder.WebHost.ConfigureKestrel(k => {
            k.ListenAnyIP(int.Parse(Constant.PROXY_PORT)); // PORT HANDLE
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(myAllowSpecificOrigins,
                policyBuilder =>
                {
                    Console.WriteLine(allowedOrigins);
                    if (allowedOrigins != null)
                    {
                        policyBuilder.WithOrigins(allowedOrigins);
                    }
                    else
                    {
                        policyBuilder.AllowAnyOrigin();
                    }
                });
        });

        var app = builder.Build();
        app.Use(async (context, next) =>
        {
            var connection = context.Connection;
            if (connection.RemoteIpAddress != null &&
                (connection.RemoteIpAddress.Equals(IPAddress.Loopback) ||
                 connection.RemoteIpAddress.Equals(IPAddress.IPv6Loopback)))
            {
                await next();
            }
            else
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Man, what are you doing here?, i just want to play csgo with 800 ping.");
            }
        });

        app.UseRouting();
        app.UseCors(myAllowSpecificOrigins);
        app.UseOutputCache();
        app.UseAuthentication();
        app.MapControllers();

        app.MapGet("/player", async context =>
        {
            string CURRENT_DIR = AppDomain.CurrentDomain.BaseDirectory;

            var filePath = Path.Combine(CURRENT_DIR, Constant.PlayerHtmlPath); // Player html directory

            if (File.Exists(filePath))
            {
                var fileContents = File.ReadAllText(filePath);
                await context.Response.WriteAsync(fileContents);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
        });


        app.MapGet("hls/source.m3u8", context => {
            context.Response.Redirect(Constant.baseProxyAddress + m3u8_url);
            return Task.CompletedTask;
        });

        app.MapGet("hls/subtitle", context => {
            context.Response.Redirect(vtt_url);
            return Task.CompletedTask;
        });


        ProcessStartInfo player = new ProcessStartInfo
        {
            FileName = Constant.baseProxyAddress + "player",
            UseShellExecute = true
        };
        Process.Start(player);

        Console.CancelKeyPress += (sender, e) =>
        {
            e.Cancel = true;
            app.StopAsync();
            Thread.Sleep(3000);
            Console.Clear();
            Thread thread = new Thread(new ThreadStart(Program.MenuHandlerInvoke));
            thread.Start();
   
        };

        app.Run();

    }
}