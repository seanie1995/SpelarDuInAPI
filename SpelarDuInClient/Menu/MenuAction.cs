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
                    Console.WriteLine("\x1b[31;5m  W\x1b[32;5me\x1b[33;5ml\x1b[34;5mc\x1b[35;5mo\x1b[36;5mm\x1b[37;5me \x1b[1;31;5mt\x1b[1;32;5mo \x1b[1;31;5mS\x1b[1;34;5mD\x1b[1;35;5mI-\x1b[1;36;5mA\x1b[1;37;5mP\x1b[1;31;5mI\x1b[0m");
                    await MenuAesthetics.UnderLineHeaderButtonsAsync();
                    //await MenuAesthetics.ChooseOptions();
                    string[] options = { "[List all users]", "[Create new user]", "[Exit]" };                                                                                  
                    int selectedIndex = MenuHelper.RunMenu(options, false, true, 0, 4); 

                    UserViewModel selectedUser = null;
                    switch (selectedIndex)
                    {
                        case 0:
                            Console.Clear();
                            await Console.Out.WriteLineAsync(" Choose one of the following user:");
                            await MenuAesthetics.UnderLineHeaderButtonsAsync();
                            List<UserViewModel> userList = await UserMethods.GetAllUsersForMenyAsync(client);
                            if (userList != null)
                            {
                                List<string> userStrings = new List<string>();
                                foreach (UserViewModel user in userList)
                                {
                                    userStrings.Add($"{user.UserName}");
                                    //await Console.Out.WriteLineAsync($"Username: {user.UserName}");
                                }

                                string[] userOption = userStrings.ToArray();
                                int selecteduserIndex = MenuHelper.RunMenu(userOption, false, false, 0, 3);

                                if (selecteduserIndex >= 0 && selecteduserIndex < userList.Count())
                                {
                                    //Get the selected user 
                                    selectedUser = userList[selecteduserIndex];
                                    //selectedUser = await UserMethods.SelectUserAsync(client, selectedUser.Id);
                                }
                                if (selectedUser == null)
                                {
                                    await Console.Out.WriteLineAsync($"\u001b[31mUser:[{userId}] not found! Press enter to try again.\u001b[0m");
                                    Console.ReadKey();
                                    continue;
                                }
                                await UserLogInMenu.UsersLogInMenuAsync(client, selectedUser.Id, selectedUser);
                                
                            }
                            break;

                        //await UserMethods.ListAllUsersAsync(client);
                        ////await UserMethods.ShowAllUsersAllInfo(client);
                        case 1:
                            Console.Clear();
                            await UserMethods.CreateNewUserAsync(client);
                            //continue;
                            break;
                        case 2:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.Clear();
                            await Console.Out.WriteLineAsync("\x1b[31mYa doin something wrong\u001b[0m");
                            await MenuAesthetics.EnterBackToMenuAsync();
                            Console.ReadLine();
                            //continue;
                            break;
                    }                 
                }
            }
        }
    }
}
