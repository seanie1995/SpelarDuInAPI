using SpelarDuInAPIClient.Methods;
using SpelarDuInClient.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using SpelarDuInClient.Models.ViewModels;

namespace SpelarDuInClient.Menu
{
    public class MenuAction
    {
        public async static Task MainMenu()
        {
            using (HttpClient client = new HttpClient())
            {
                // Connecting to the API
                client.BaseAddress = new Uri("https://localhost:7107/");  // Connecting to the API
                int userId = 0;

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("\x1b[31;5mW\x1b[32;5me\x1b[33;5ml\x1b[34;5mc\x1b[35;5mo\x1b[36;5mm\x1b[37;5me \x1b[1;31;5mt\x1b[1;32;5mo \x1b[1;31;5mS\x1b[1;34;5mD\x1b[1;35;5mI-\x1b[1;36;5mA\x1b[1;37;5mP\x1b[1;31;5mI\x1b[0m");
                    await Console.Out.WriteLineAsync("-----------------------------------------------------------------");
                    await Console.Out.WriteLineAsync("Chose one of the options:");
                    await Console.Out.WriteLineAsync("\x1b[33m[1].List all users \n[2].Create new user:\n\u001b[0m");

                    string response = Console.ReadLine();
                    UserViewModel selectedUser = null;
                    // Lists all users
                    if (response == "1")
                    {
                        Console.Clear();
                        await Console.Out.WriteLineAsync("All users in the system:");
                        await Console.Out.WriteLineAsync("--------------------------------");
                        await UserMethods.ListAllUsersAsync(client);
                        //await UserMethods.ShowAllUsersAllInfo(client);
                        await Console.Out.WriteLineAsync("Select user by ID");
                        string strUserId = Console.ReadLine();
                        userId = Convert.ToInt32(strUserId);
                        selectedUser = await UserMethods.SelectUserAsync(client, userId);
                        if (selectedUser == null)
                        {
                            await Console.Out.WriteLineAsync($"\u001b[31mUser:[{userId}] not found! Press enter to try again.\u001b[0m");
                            Console.ReadKey();
                            continue;
                        }
                        await UserLogInMenu.UsersLogInMenuAsync(client, userId, selectedUser);
                        //break;

                    }// Create new user
                    else if (response == "2")
                    {
                        Console.Clear();
                        await UserMethods.CreateNewUserAsync(client);
                        continue;
                    }
                    else if (response != "1" || response != "2")
                    {
                        Console.Clear();
                        await Console.Out.WriteLineAsync("\x1b[31mYa doin something wrong\u001b[0m");
                        await Console.Out.WriteLineAsync("Press enter to go back!");
                        Console.ReadLine();
                        continue;
                    }
                }
            }
        }
    }
}
