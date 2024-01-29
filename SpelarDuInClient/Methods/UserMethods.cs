using Microsoft.EntityFrameworkCore;
using SpelarDuInAPIClient.Models.DTO;
using SpelarDuInClient.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpelarDuInAPIClient.Methods
{
    public class UserMethods
    {
        public static async Task ListAllUsersAsync(HttpClient client)
        {

            HttpResponseMessage response = await client.GetAsync("/user"); // Anropar API endpoint som vi skapat i vår API.

            if (!response.IsSuccessStatusCode)
            {
                //throw new Exception($"Failed to list users {response.StatusCode}");
                await Console.Out.WriteLineAsync($"Failed to list users {response.StatusCode}");
            }

            string content = await response.Content.ReadAsStringAsync();

            UserViewModel[] allUsers = JsonSerializer.Deserialize<UserViewModel[]>(content); // Deserialize JSON object retrieved from API

            foreach (var user in allUsers)
            {
                await Console.Out.WriteLineAsync($"\u001b[33mId:{user.Id}:\t{user.UserName}\u001b[0m");
            }

        }

        public static async Task CreateNewUserAsync(HttpClient client)
        {
            await Console.Out.WriteLineAsync("CREATING NEW USER");
            await Console.Out.WriteLineAsync("------------------------\n");
            await Console.Out.WriteLineAsync("Enter desired username:");

            string name = Console.ReadLine();

            if (name == null)
            {
                await Console.Out.WriteLineAsync("Name cannot be empty");
            }

            UserDto newUser = new UserDto()
            {
                UserName = name
            };

            string json = JsonSerializer.Serialize(newUser); // Seralize DTO into JSON. 

            StringContent jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/user", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                await Console.Out.WriteLineAsync($"Failed to create user (status code {response.StatusCode})");
            }
            Console.Clear();
            await Console.Out.WriteLineAsync($"\x1b[32mUsername[{name}] was created!\x1b[0m");
            await Console.Out.WriteLineAsync("Press enter to go back to main menu");
            Console.ReadLine();

        }

        public static async Task ShowAllUsersAllInfoAsync(HttpClient client)
        {
                HttpResponseMessage response = await client.GetAsync("/user/allinfo");
                if (!response.IsSuccessStatusCode)
                {
                    await Console.Out.WriteLineAsync($"Failed to list users {response.StatusCode}");
                }

                string content = await response.Content.ReadAsStringAsync();
                UserViewModelAllInfo[] allInfoUsers = JsonSerializer.Deserialize<UserViewModelAllInfo[]>(content);

                foreach (var user in allInfoUsers)
                {
                    Console.WriteLine($"User: \x1b[33m{user.UserName}\x1b[0m");

                    Console.WriteLine("Genres:");
                    if (user.Genres != null && user.Genres.Any())
                    {
                        foreach (var genre in user.Genres)
                        {
                            Console.WriteLine($"\x1b[33m{genre.GenreName}\x1b[0m");
                        }
                    }
                    else
                    {
                        Console.WriteLine("  \x1b[31mNo genres available\x1b[0m");
                    }

                    Console.WriteLine("Artists:");
                    if (user.Artists != null && user.Artists.Any())
                    {
                        foreach (var artist in user.Artists)
                        {
                            Console.WriteLine($"\x1b[33mArtist:{artist.ArtistName} \n Desciption:{artist.Description}\x1b[0m");
                        }
                    }
                    else
                    {
                        Console.WriteLine(" \x1b[31mNo artists available\x1b[0m");
                    }

                    Console.WriteLine("Tracks:");
                    if (user.Tracks != null && user.Tracks.Any())
                    {
                        foreach (var track in user.Tracks)
                        {
                            Console.WriteLine($"\x1b[33m{track.TrackTitle}\x1b[0m");
                        }
                    }
                    else
                    {
                        Console.WriteLine("  \x1b[31mNo tracks available\x1b[0m");
                    }
                    Console.WriteLine();
                    Console.WriteLine("-----------------------------");
                }
        }

        public static async Task ShowAllUsersAllInfoOneUserAsync(HttpClient client, int userId)
        {
            HttpResponseMessage response = await client.GetAsync($"/user/allinfo/{userId}");
            if (!response.IsSuccessStatusCode)
            {
                await Console.Out.WriteLineAsync($"Failed to list users {response.StatusCode}");
            }

            string content = await response.Content.ReadAsStringAsync();
            UserViewModelAllInfo[] allUserInfo = JsonSerializer.Deserialize<UserViewModelAllInfo[]>(content);

            foreach (var user in allUserInfo)
            {
                Console.WriteLine($"User: \x1b[33m{user.UserName}\x1b[0m");

                Console.WriteLine("Genres:");
                if (user.Genres != null && user.Genres.Any())
                {
                    foreach (var genre in user.Genres)
                    {
                        Console.WriteLine($"\x1b[33m{genre.GenreName}\x1b[0m");
                    }
                }
                else
                {
                    Console.WriteLine("  \x1b[31mNo genres available\x1b[0m");
                }

                Console.WriteLine("Artists:");
                if (user.Artists != null && user.Artists.Any())
                {
                    foreach (var artist in user.Artists)
                    {
                        Console.WriteLine($"\x1b[33mArtist:{artist.ArtistName} \n Desciption:{artist.Description}\x1b[0m");
                    }
                }
                else
                {
                    Console.WriteLine(" \x1b[31mNo artists available\x1b[0m");
                }

                Console.WriteLine("Tracks:");
                if (user.Tracks != null && user.Tracks.Any())
                {
                    foreach (var track in user.Tracks)
                    {
                        Console.WriteLine($"\x1b[33m{track.TrackTitle}\x1b[0m");
                    }
                }
                else
                {
                    Console.WriteLine("  \x1b[31mNo tracks available\x1b[0m");
                }
                Console.WriteLine();
                Console.WriteLine("-----------------------------");
            }
        }

        public static async Task<UserViewModel> SelectUserAsync(HttpClient client, int userId)
        {
            HttpResponseMessage response = await client.GetAsync("/user"); // Anropar API endpoint som vi skapat i vår API.

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to list users {response.StatusCode}");
            }

            string content = await response.Content.ReadAsStringAsync();

            UserViewModel[] allUsers = JsonSerializer.Deserialize<UserViewModel[]>(content); // Deserialize JSON object retrieved from API

            UserViewModel selectedUser = allUsers
                .Where(i => i.Id == userId)
                .FirstOrDefault();

            if (selectedUser != null)
            {
                await Console.Out.WriteLineAsync("Requested user not found");
            }

            return selectedUser;

        }

        public static async Task ConnectUserToOneGenreAsync(HttpClient client, int userId)
        {
            Console.Clear();
            await Console.Out.WriteLineAsync("Adding Genre to user");
            await Console.Out.WriteLineAsync("----------------------");
            Console.WriteLine($"\u001b[33mEnter genre ID\u001b[0m");
            string genreId = Console.ReadLine();
            HttpResponseMessage response = await client.PostAsync($"/user/{userId}/genre/{genreId}", null);
            if (response.IsSuccessStatusCode)
            {
                Console.Clear();
                Console.WriteLine($"\x1b[32mUser connected to the genre successfully!\x1b[0m");
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"\x1b[31mFailed to connect. Statuscode: {response.StatusCode}\x1b[0m");
            }
        }

        public static async Task ConnectUserToOneArtistAsync(HttpClient client, int userId)
        {
            Console.Clear();
            await Console.Out.WriteLineAsync("Adding Artist to user");
            await Console.Out.WriteLineAsync("----------------------");
            Console.WriteLine($"\u001b[33mEnter artist ID\u001b[0m");
            string artistId = Console.ReadLine();
            HttpResponseMessage response = await client.PostAsync($"/user/{userId}/artist/{artistId}", null);
            if (response.IsSuccessStatusCode)
            {
                Console.Clear();
                Console.WriteLine($"\x1b[32mUser connected to the artist successfully!\x1b[0m");
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"\x1b[31mFailed to connect. Statuscode: {response.StatusCode}\x1b[0m");
            }
        }

        public static async Task ConnectUserToOneTrackAsync(HttpClient client, int userId)
        {
            Console.Clear();
            await Console.Out.WriteLineAsync("Adding Track to user");
            await Console.Out.WriteLineAsync("----------------------");
            Console.WriteLine($"\u001b[33mEnter track ID\u001b[0m");
            string trackId = Console.ReadLine();
            HttpResponseMessage response = await client.PostAsync($"/user/{userId}/track/{trackId}", null);
            if (response.IsSuccessStatusCode)
            {
                Console.Clear();
                Console.WriteLine($"\x1b[32mUser connected to the track successfully!\x1b[0m");
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"\x1b[31mFailed to connect. Statuscode: {response.StatusCode}\x1b[0m");
            }
        }

        //public static async Task<UserViewModel> SelectUserAsync(HttpClient client, int userId)
        //{
        //    HttpResponseMessage response = await client.GetAsync("/user"); // Anropar API endpoint som vi skapat i vår API.

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        throw new Exception($"Failed to list users {response.StatusCode}");
        //    }

        //    string content = await response.Content.ReadAsStringAsync();

        //    UserViewModel[] allUsers = JsonSerializer.Deserialize<UserViewModel[]>(content); // Deserialize JSON object retrieved from API

        //    UserViewModel selectedUser = allUsers
        //        .Where(i => i.Id == userId)
        //        .FirstOrDefault();

        //    if (selectedUser != null)
        //    {
        //        await Console.Out.WriteLineAsync("Requested user not found");
        //    }

        //    return selectedUser;

        //}
    }
}
