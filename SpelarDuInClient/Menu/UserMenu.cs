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
                await MenuAesthetics.UnderLineHeaderButtonsAsync();
                await MenuAesthetics.ChooseOptions();
                string[] options = { "[Favorites🤍]", "[Add genre to🤍]", "[Add Artist to🤍]", "[Add track to🤍]", "[Back]" };
                int choice = MenuHelper.RunMenu(options, false, true, 0, 4);
                switch (choice)
                {
                    case 0:
                        await UserMethods.ShowAllUsersAllInfoOneUserAsync(client, userId);
                        break;
                    case 1:
                        await UserMethods.ConnectUserToOneGenreAsync(client, userId);
                        break;
                    case 2:
                        await UserMethods.ConnectUserToOneArtistAsync(client, userId);
                        break;
                    case 3:
                        await UserMethods.ConnectUserToOneTrackAsync(client, userId);
                        break;
                    case 4:
                        run = false;
                        break;
                }
            }
        }
    }
}
