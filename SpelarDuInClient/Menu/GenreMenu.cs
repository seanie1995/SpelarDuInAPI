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
                await Console.Out.WriteLineAsync($" Welcome {user.UserName}");
                await MenuAesthetics.UnderLineHeaderButtonsAsync();
                string[] options = { "[List your genres]", "[Create new genre]", "[List all genres]", "[Back]" };
                int selectedIndex = MenuHelper.RunMenu(options, false, true, 0, 4);
                switch (selectedIndex)
                {
                    case 0:
                        await GenreMethods.ListUserGenresAsync(client, userId, user);
                        break;
                    case 1:
                        await GenreMethods.CreateNewGenreAsync(client, userId, user);
                        break;
                    case 2:
                        await GenreMethods.ListAllGenresAsync(client, userId, user);
                        break;
                    case 3:
                        await UserLogInMenu.UsersLogInMenuAsync(client, userId, user);
                        break;
                }

            }
        }
    }
}
