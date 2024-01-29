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
                await Console.Out.WriteLineAsync($"Welcome {user.UserName}");
                await Console.Out.WriteLineAsync("-----------------------------");
                await Console.Out.WriteLineAsync("Click one of the following sub-menus:");
                string[] options = { "User", "Genre", "Artist", "Track", "Discography", "Exit" };   //Meny options
                //MenuHelper mainMeny = new MenuHelper(prompt, options);
                int selectedIndex = MenuHelper.RunMeny(options, false, true, 1, 13);     //Run method that registers arrowkeys and displays the options. 


                switch (selectedIndex)
                {
                    case 0:
                        UserMenu.UsersMenuAsync(client, userId, user);
                        break;
                    case 1:
                        GenreMenu.GenreMenuAsync(client, userId, user);
                        break;
                    case 2:
                        ArtistMenu.ArtistMenuAsync(client, userId, user);
                        break;
                    case 3:
                        //Track methods
                        break;
                    case 4:
                        DiscographyMenu.DiscographyMenuAsync(client, user);
                        break;
                    case 5:
                        ExitProgram();
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