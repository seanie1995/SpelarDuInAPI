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
        public static async Task GenreMenuAsync(HttpClient client, UserViewModel user)
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
                    await Console.Out.WriteLineAsync($"\u001b[31mInvalid Input![{choice}]\u001b[0m");
                    Console.ReadKey();
                    continue;
                }
                else
                {
                    switch (choice)
                    {
                        case "1":
                            await GenreMethods.ListUserGenresAsync(client, user);
                            break;
                        case "2":
                            await GenreMethods.CreateNewGenreAsync(client,  user);                                                      
                            break;
                        case "3":
                            await GenreMethods.ListAllGenresAsync(client, user);
                            break;
                        case "4":
                            await UserLogInMenu.UsersLogInMenuAsync(client,  user);
                            break;
                    }
                }
                
            }
        }
    }
}
