using SpelarDuInAPIClient.Methods;
using SpelarDuInAPIClient.Models;
using SpelarDuInClient.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpelarDuInClient.Menu
{
    public class GenreMenu
    {
        public static async Task GenreMenuAsync(HttpClient client, int userId, UserViewModel user)
        {
            bool run = true;
            while (run)
            {
                Console.Clear();
                await Console.Out.WriteLineAsync($"Welcome {user.UserName}");
                await Console.Out.WriteLineAsync("-----------------------------");
                await Console.Out.WriteLineAsync("Choose one of the following:\n\u001b[33m[1] List your genres \n[2] Create new genre\n[3] Go back\u001b[0m");
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
                            await GenreMethods.ListUserGenresAsync(client, userId);
                            break;
                        case "2":
                            await GenreMethods.CreateNewGenreAsync(client, userId);
                            
                           
                            break;                      
                        case "3":
                            run = false;
                            break;
                    }
                }
                await Console.Out.WriteLineAsync("Press enter to go back to main menu!!");
                Console.ReadKey();
            }
        }
    }
}
