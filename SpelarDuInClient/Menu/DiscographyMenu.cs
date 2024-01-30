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
                await Console.Out.WriteLineAsync($" Welcome {user.UserName}");
                await MenuAesthetics.UnderLineHeaderButtonsAsync();
                string[] options = { "[List some albums by an artist]", "[Back]" };
                int selectedIndex = MenuHelper.RunMenu(options, false, true, 0, 4);
                switch (selectedIndex)
                {
                    case 0:
                        try
                        {
                            await DiscographyMethods.ListAlbumsAsync(client, userId, user);
                        }
                        catch (Exception ex)
                        {
                            await Console.Out.WriteLineAsync($"Something went wrong!  \nStatusCode:{ex.Message}");
                            await MenuAesthetics.EnterBackToMenuAsync();
                        }
                        break;
                    case 1:
                        await UserLogInMenu.UsersLogInMenuAsync(client, userId, user);
                        break;
                }
            }
        }
    }
}

