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
    internal class ArtistMenu
    {
        public static async Task ArtistMenuAsync(HttpClient client, int userId, UserViewModel user)
        {
            Console.Clear();
            bool run = true;
            while (run)
            {
                Console.Clear();
                await Console.Out.WriteLineAsync($"Welcome {user.UserName}");
                await Console.Out.WriteLineAsync("-----------------------------");
                await Console.Out.WriteLineAsync("Choose one of the following:\n\u001b[33m[1] Add new artist\n[2] List user's artists\n[3] List all artists\n[4] Go back\u001b[0m");
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
                        case "1":// Add new artist
                            await ArtistMethods.AddNewArtistAysnc(client);
                            break;
                        case "2":// list artists related to a specific user
                            await ArtistMethods.ListUserArtistsAsync(client, userId);
                            break;
                        case "3"://list all artists
                            await ArtistMethods.ListAllArtistsAsync(client);
                            break;
                        case "4":
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
