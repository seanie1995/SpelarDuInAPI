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
        public static async Task ArtistMenuAsync(HttpClient client, UserViewModel user)
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
                    await Console.Out.WriteLineAsync($"\u001b[31mInvalid Input![{choice}]\u001b[0m");
                    Console.ReadKey();
                    continue;
                }
                else
                {
                    switch (choice)
                    {
                        case "1":// Add new artist
                            await ArtistMethods.AddNewArtistAysnc(client, user);
                            break;
                        case "2":// list artists related to a specific user
                            await ArtistMethods.ListUserArtistsAsync(client, user);
                            break;
                        case "3"://list all artists
                            await ArtistMethods.ListAllArtistsAsync(client, user);
                            break;
                        case "4":
                            await UserLogInMenu.UsersLogInMenuAsync(client, user);
                            break;
                    }
                }
            }
        }
    }
}
