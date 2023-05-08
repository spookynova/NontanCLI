using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NontanCLI.Utils;
using Spectre.Console;

namespace NontanCLI.Feature
{
    public class MenuHandler
    {
        [Obsolete]
        public string MenuHandlerInvoke()
        {

            // figlet 

            AnsiConsole.Write(
                new FigletText("NontanCLI")
                    .LeftJustified()
                    .Color(Color.Yellow));

            AnsiConsole.MarkupLine("[bold yellow]Welcome to NontanCLI[/]");

            AnsiConsole.MarkupLine("[bold white]A Simple Console App for streaming Anime[/]");

            AnsiConsole.MarkupLine("[bold white]Made with Love by evnx32[/]");
            // Project Link
            AnsiConsole.MarkupLine("[bold white]Project Link : https://github.com/evnx32/NontanCLI [/]");
            
            AnsiConsole.MarkupLine($"[bold white]Version :[/] [bold green]{Program.version}[/]" + $" ({Program.buildVersion})");

            AnsiConsole.MarkupLine($"[bold white]Server :[/] [bold green]{Constant.provider}[/]\n\n");


            var _prompt = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[green]Select Menu Available[/]?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more menu)[/]")
                .AddChoices(new[] {
                    "Popular Anime","Trending Anime", "Recent Anime", "Search Anime" , "Airing Schedule","Exit"
                }));

            return _prompt;
        }
    }
}
