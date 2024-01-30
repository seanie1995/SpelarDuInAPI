using SpelarDuInAPIClient.Methods;
using SpelarDuInClient.Methods;
using SpelarDuInClient.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpelarDuInClient.Menu
{
    internal class TrackMenu
    {
        public static async Task TrackMenuAsync(HttpClient client, UserViewModel user)
        {
            bool run = true;
            while (run)
            {
                Console.Clear();
                await Console.Out.WriteLineAsync($"Welcome {user.UserName}");
                await Console.Out.WriteLineAsync("-----------------------------");
                await Console.Out.WriteLineAsync("Choose one of the following:\n\u001b[33m[1] Add track\n[2] Show all tracks connected to user\n[3] Go back\u001b[0m");
                string choice = Console.ReadLine();
                if (choice != "1" && choice != "2" && choice != "3" && choice != "4")
                {
                    await Console.Out.WriteLineAsync($"\u001b[31mInvalid Input![{choice}]\u001b[0m");
                    Console.ReadKey();
                    continue;
                }
                else
                {
                    switch (choice)
                    {
                        case "1":
                           await ClientTrackHandler.AddtrackAsync(client, user);
                            break;
                        case "2":
                           await ClientTrackHandler.GetAlltracksFromSingleUserAsync(client, user);
                            break;                      
                        case "3":
                            await UserLogInMenu.UsersLogInMenuAsync(client, user);
                            break;
                           

                    }
                }
                
            }
        }
    }
}
