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
        public static async Task TrackMenuAsync(HttpClient client, int userId, UserViewModel user)
        {
            bool run = true;
            while (run)
            {
                Console.Clear();
                await Console.Out.WriteLineAsync($" Welcome {user.UserName}");
                await MenuAesthetics.UnderLineHeaderButtonsAsync();
                string[] options = { "[Add track]", "[Show all tracks connected to user]", "[Back]" };
                int selectedIndex = MenuHelper.RunMenu(options, false, true, 0, 4);
                switch (selectedIndex)
                {
                    case 0:
                        await ClientTrackHandler.AddtrackAsync(client, userId);
                        break;
                    case 1:
                        await ClientTrackHandler.GetAlltracksFromSingleUserAsync(client, userId);
                        break;
                    case 2:
                        await UserLogInMenu.UsersLogInMenuAsync(client, userId, user);
                        break;
                }
            }
        }
    }
}
