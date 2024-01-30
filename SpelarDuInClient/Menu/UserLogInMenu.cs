using Microsoft.EntityFrameworkCore;
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
    public class UserLogInMenu
    {
        public static async Task UsersLogInMenuAsync(HttpClient client, int userId, UserViewModel user)
        {
            bool run = true;
            while (run)
            {
                Console.Clear();
                await Console.Out.WriteLineAsync($" Welcome {user.UserName}");
                await MenuAesthetics.UnderLineHeaderButtonsAsync();
                await MenuAesthetics.ChooseOptions();
                string[] options = { "[User]", "[Genre]", "[Artist]", "[Track]", "[Discography]", "[Main menu]" };
                //MenuHelper mainMeny = new MenuHelper(prompt, options);
                int selectedIndex = MenuHelper.RunMenu(options, false, true, 0, 4); 
                switch (selectedIndex)
                {
                    case 0:
                        await UserMenu.UsersMenuAsync(client, userId, user);
                        break;
                    case 1:
                        await GenreMenu.GenreMenuAsync(client, userId, user);
                        break;
                    case 2:
                        await ArtistMenu.ArtistMenuAsync(client, userId, user);
                        break;
                    case 3:
                        await TrackMenu.TrackMenuAsync(client, userId, user);
                        break;
                    case 4:
                        await DiscographyMenu.DiscographyMenuAsync(client, userId, user);
                        break;
                    case 5:
                        await MenuAction.MainMenu();
                        //run = false;
                       // ExitProgram();
                        break;
                }
            }
        }
        public static void ExitProgram() //Exit the game
        {
            Console.WriteLine("\nPress any key to exit");
            Console.ReadKey(true);
            Environment.Exit(0);
        }
    }
}