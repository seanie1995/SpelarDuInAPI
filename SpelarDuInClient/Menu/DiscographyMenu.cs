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
        public static async Task DiscographyMenuAsync(HttpClient client, UserViewModel user)
        {
            bool run = true;
            while (run)
            {
                Console.Clear();
                await Console.Out.WriteLineAsync($" Welcome {user.UserName}");
                await MenuAesthetics.UnderLineHeaderButtonsAsync();
                string[] options = { "[Discover Artist Albums]", "[Back]" };
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
                            await DiscographyMethods.ListAlbumsAsync(client, user);
                            break;                      
                        case "2":
                            await UserLogInMenu.UsersLogInMenuAsync(client, user);
                            break;
                    }
                }
                
            }
        }
    }
}

