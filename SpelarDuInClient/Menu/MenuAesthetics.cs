using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpelarDuInClient.Menu
{
    public static class MenuAesthetics
    {
        public static async Task UnderLineHeaderAsync()
        {
            await Console.Out.WriteLineAsync("");
            //await Console.Out.WriteLineAsync("--------------------------------");
        }
        public static async Task UnderLineHeaderButtonsAsync()
        {
            await Console.Out.WriteLineAsync("");
            //await Console.Out.WriteLineAsync(" --------------------------------");
        }

        public static async Task EnterBackToMenuAsync()
        {
            Console.CursorVisible = false;
            await Console.Out.WriteLineAsync("\nPress enter to return to menu");
            Console.ReadLine();
        }
        public static async Task ChooseOptions()
        {
            //await Console.Out.WriteLineAsync(" Choose one of the following options:\n");
            await Console.Out.WriteLineAsync("");
        }
    }
}
