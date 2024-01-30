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
                await Console.Out.WriteLineAsync($" Welcome {user.UserName}");
                await MenuAesthetics.UnderLineHeaderButtonsAsync();
                await MenuAesthetics.ChooseOptions();
                string[] options = { "[Add new artist]", "[List user's artists]", "[List all artists]", "[Back]" };   //Meny options                                                                                                 
                int selectedIndex = MenuHelper.RunMenu(options, false, true, 0, 4);
                switch (selectedIndex)
                {
                    case 0:// Add new artist
                        await ArtistMethods.AddNewArtistAysnc(client, userId, user);
                        break;
                    case 1:// list artists related to a specific user
                        await ArtistMethods.ListUserArtistsAsync(client, userId, user);
                        break;
                    case 2://list all artists
                        await ArtistMethods.ListAllArtistsAsync(client, userId, user);
                        break;
                    case 3:
                        await UserLogInMenu.UsersLogInMenuAsync(client, userId, user);
                        break;

                }
            }
        }
    }
}
