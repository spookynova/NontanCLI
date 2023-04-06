using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace NontanCLI.Feature
{
    internal class Menu
    {
        [Obsolete]
        public static string MenuHandler()
        {

            // figlet 

            AnsiConsole.Write(
                new FigletText("NontanCLI")
                    .LeftJustified()
                    .Color(Color.Yellow));

            var _prompt = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[green]Select Menu Available[/]?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more menu)[/]")
                .AddChoices(new[] {
                    "Popular","Trending", "Search", "Bookmark","Exit"
                }));

            return _prompt;
        }
    }
}
