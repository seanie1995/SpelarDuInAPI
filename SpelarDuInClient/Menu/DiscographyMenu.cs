using SpelarDuInClient.Methods;
using SpelarDuInClient.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpelarDuInClient.Menu
{
    internal class DiscographyMenu
    {
        public static async Task DiscographyMenuAsync(HttpClient client, int userId, UserViewModel user)
        {
            bool run = true;
            while (run)
            {
                Console.Clear();
                await Console.Out.WriteLineAsync($"Welcome {user.UserName}");
                await Console.Out.WriteLineAsync("-----------------------------");
                await Console.Out.WriteLineAsync("Choose one of the following:\n\u001b[33m[1] List some albums by an artist \n[2] Go back\u001b[0m");
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
                            await DiscographyMethods.ListAlbumsAsync(client, userId, user);
                            break;                      
                        case "2":
                            await UserLogInMenu.UsersLogInMenuAsync(client, userId, user);
                            break;
                    }
                }
                
            }
        }
    }
}
