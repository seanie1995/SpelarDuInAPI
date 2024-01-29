using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SpelarDuInClient.Menu
{
    internal class WelcomeMenu
    {
        public static async Task WelcomeSignMenu()
        {
           var cts = new CancellationTokenSource();
            var welcomeSignTask = Task.Run(()=> MenuRepositoryHandler.WelcomeSign(cts.Token));

            while (true)
            {
               
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                   cts.Cancel();
                    break;
                }
                
            }
            await welcomeSignTask;
            await MenuAction.MainMenu();


        }
    }
}
