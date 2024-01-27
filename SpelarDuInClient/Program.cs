using DiscographyViewerAPI.Models.ViewModels;
using SpelarDuInAPIClient.Methods;
using SpelarDuInAPIClient.Models;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace SpelarDuInAPIClient
{
    internal class Program
    {
        static async Task Main(string[] args)
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
                    await Console.Out.WriteLineAsync("\x1b[33mEnter [1] to list users or [2] create new user:\u001b[0m");

                    string response = Console.ReadLine();

                    // Lists all users
                    if (response == "1")
                    {
                        Console.Clear();
                        await UserMethods.ListAllUsersAsync(client);
                        //await UserMethods.ShowAllUsersAllInfo(client);
                        await Console.Out.WriteLineAsync("Select your ID.");
                        string strUserId = Console.ReadLine();
                        userId = Convert.ToInt32(strUserId);

                        break;

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

                while (true)
                {
                    //egen metod där den visar användarens innehåll. samt alternativ att lägga 
                    Console.Clear() ;
                    await UserMethods.ShowAllUsersAllInfoOneUser(client, userId);
                    Console.WriteLine();
                    await Console.Out.WriteLineAsync($"Enter 1 to add genre, 2 to list your genres:");
                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "1": // AddGenre

                            await GenreMethods.AddGenreAsync(client);

                            break;
                        case "2": // List Genres

                            await GenreMethods.ListUserGenresAsync(client, userId);

                            break;
                        case "3": // Add artist

                            break;

                        case "4": // list artist

                            break;

                        case "5": // add track
                            break;

                        case "6": // list track
                            break;
                        default:
                            Console.WriteLine("Invalid input. Please enter 1, 2, or 3.");

                            break;
                    }

                }

            }
        }
    }
}
