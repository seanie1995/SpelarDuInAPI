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
    internal class UserMenu
    {
        public static async Task UsersMenuAsync(HttpClient client, int userId, UserViewModel user)
        {
            bool run = true;
            while (run)
            {
                Console.Clear();
                await Console.Out.WriteLineAsync($" Welcome {user.UserName}");
                await Console.Out.WriteLineAsync(" -----------------------------");
               // await Console.Out.WriteLineAsync("Choose one of the following:\n\u001b[33m[1] Add genre to user\n[2] Add artist to user\n[3] Add track to user\n[4] Go back\u001b[0m");
                string[] options = { "[Add genre]", "[Add Artist]", "[Add track]", "[Go back]" };
                int choice = MenuHelper.RunMenu(options, false, true, 0, 4);//Meny options
               // string choice = Console.ReadLine();
                //if (choice != "1" && choice != "2" && choice != "3" && choice != "4")
                //{
                //    await Console.Out.WriteLineAsync($"\u001b[31mInvalid Input![{choice}]\u001b[0m");
                //    Console.ReadKey();
                //    continue;
                //}
                //else
                {
                    switch (choice)
                    {
                        case 0:
                            await UserMethods.ConnectUserToOneGenreAsync(client, userId);
                            break;
                        case 1:
                            await UserMethods.ConnectUserToOneArtistAsync(client, userId);
                            break;
                        case 2:
                            await UserMethods.ConnectUserToOneTrackAsync(client, userId);
                            break;
                        case 3:
                            run = false;
                            break;
                        //case "5": // Add new artist
                        //    await ArtistMethods.AddNewArtistAysnc(client);
                        //    break;
                        //case "6":// list artists related to a specific user
                        //    await ArtistMethods.ListUserArtistsAsync(client, userId);
                        //    break;
                        //case "7"://list all artists
                        //    await ArtistMethods.ListAllArtistsAsync(client);
                        //    break;

                    }
                }
                //await Console.Out.WriteLineAsync("Press enter to go back!!");
                //Console.ReadKey();
            }
        }
    }
}
