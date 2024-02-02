using Microsoft.EntityFrameworkCore;
using SpelarDuInAPIClient.Models.DTO;
using SpelarDuInClient.Menu;
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
                await Console.Out.WriteLineAsync($"Failed to list users {response.StatusCode}");
            }

            string content = await response.Content.ReadAsStringAsync();

            UserViewModel[] allUsers = JsonSerializer.Deserialize<UserViewModel[]>(content); // Deserialize JSON object retrieved from API

            //Print out on client
            foreach (var user in allUsers)
            {
                await Console.Out.WriteLineAsync($"\u001b[33mId:{user.Id}:\t{user.UserName}\u001b[0m");
            }
        }

        public static async Task<List<UserViewModel>> GetAllUsersForMenyAsync(HttpClient client)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("/user"); // Calling API endpoint.

                if (!response.IsSuccessStatusCode)
                {
                    await Console.Out.WriteLineAsync($"Failed to list users {response.StatusCode}");
                    return new List<UserViewModel>();
                }

                string content = await response.Content.ReadAsStringAsync();

                //Deserialize json directly to list
                List<UserViewModel> allUsers = JsonSerializer.Deserialize<List<UserViewModel>>(content);

                return allUsers;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"An error occured: {ex.Message}");
                return new List<UserViewModel>();
            }

        }

        public static async Task CreateNewUserAsync(HttpClient client)
        {
            Console.CursorVisible = true;
            await Console.Out.WriteLineAsync("CREATING NEW USER");
            await MenuAesthetics.UnderLineHeaderAsync();
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
            await MenuAesthetics.EnterBackToMenuAsync();
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
        } //Works but, Not in use

        public static async Task ShowOneUserAllInfoAsync(HttpClient client, int userId)
        {
            Console.Clear();
            HttpResponseMessage response = await client.GetAsync($"/user/allinfo/{userId}");
            if (!response.IsSuccessStatusCode)
            {
                await Console.Out.WriteLineAsync($"Failed to list users {response.StatusCode}");
            }

            string content = await response.Content.ReadAsStringAsync();
            UserViewModelAllInfo[] allUserInfo = JsonSerializer.Deserialize<UserViewModelAllInfo[]>(content);

            foreach (var user in allUserInfo)
            {
                await Console.Out.WriteLineAsync($"{user.UserName}'s favorites:");
                await MenuAesthetics.UnderLineHeaderAsync();

                await Console.Out.WriteLineAsync("Genres:");
                if (user.Genres != null)
                {
                    foreach (var genre in user.Genres)
                    {
                        await Console.Out.WriteLineAsync($"\x1b[37m[{genre.Id}].\x1b[33m{genre.GenreName}\x1b[0m");
                    }
                }
                else
                {
                    await Console.Out.WriteLineAsync("  \x1b[31mNo genres available\x1b[0m");
                }

                await Console.Out.WriteLineAsync("\nArtists:");
                if (user.Artists != null)
                {
                    foreach (var artist in user.Artists)
                    {
                        await Console.Out.WriteLineAsync($"\x1b[37m[{artist.Id}].\x1b[33m{artist.ArtistName} \n    \x1b[33;2m{artist.Description}\x1b[0m");
                    }
                }
                else
                {
                    await Console.Out.WriteLineAsync(" \x1b[31mNo artists available\x1b[0m");
                }
                
                await Console.Out.WriteLineAsync("\nTracks:");
                if (user.Tracks != null)
                {
                    foreach (var track in user.Tracks)
                    {
                        await Console.Out.WriteLineAsync($"\x1b[37m[{track.Id}].\x1b[33m{track.TrackTitle}\x1b[0m");
                    }
                }
                else
                {
                    await Console.Out.WriteLineAsync("  \x1b[31mNo tracks available\x1b[0m");
                }
                await MenuAesthetics.EnterBackToMenuAsync();
            }
        }

        public static async Task ConnectUserToOneGenreAsync(HttpClient client, int userId)
        {
            Console.Clear();
            Console.CursorVisible = true;
            await Console.Out.WriteLineAsync("Adding Genre to favorites❤️");
            await MenuAesthetics.UnderLineHeaderAsync();
            Console.WriteLine($"\u001b[33mEnter genre ID\u001b[0m");
            string genreId = Console.ReadLine();
            HttpResponseMessage response = await client.PostAsync($"/user/{userId}/genre/{genreId}", null);
            if (response.IsSuccessStatusCode)
            {
                Console.Clear();
                await Console.Out.WriteLineAsync($"\x1b[32mUser connected to the genre successfully!\x1b[0m"); ;
                await MenuAesthetics.EnterBackToMenuAsync();
            }
            else
            {
                Console.Clear();
                await Console.Out.WriteLineAsync($"\x1b[31mSomething went wrong! \nStatuscode: {response.StatusCode}\x1b[0m");
                await MenuAesthetics.EnterBackToMenuAsync();
            }
        }

        public static async Task ConnectUserToOneArtistAsync(HttpClient client, int userId)
        {
            Console.Clear();
            Console.CursorVisible = true;
            await Console.Out.WriteLineAsync("Adding Artist to favorites❤️");
            await MenuAesthetics.UnderLineHeaderAsync();
            Console.WriteLine($"\u001b[33mEnter artist ID\u001b[0m");
            string artistId = Console.ReadLine();
            HttpResponseMessage response = await client.PostAsync($"/user/{userId}/artist/{artistId}", null);
            if (response.IsSuccessStatusCode)
            {
                Console.Clear();
                await Console.Out.WriteLineAsync($"\x1b[32mUser connected to the artist successfully!\x1b[0m");
                await MenuAesthetics.EnterBackToMenuAsync();
            }
            else
            {
                Console.Clear();
                await Console.Out.WriteLineAsync($"\x1b[31mSomething went wrong! \nStatuscode: {response.StatusCode}\x1b[0m");
                await MenuAesthetics.EnterBackToMenuAsync();
            }
        }

        public static async Task ConnectUserToOneTrackAsync(HttpClient client, int userId)
        {
            Console.Clear();
            Console.CursorVisible = true;
            await Console.Out.WriteLineAsync("Adding Track to favorites❤️");
            await MenuAesthetics.UnderLineHeaderAsync();
            Console.WriteLine($"\u001b[33mEnter track ID\u001b[0m");
            string trackId = Console.ReadLine();
            HttpResponseMessage response = await client.PostAsync($"/user/{userId}/track/{trackId}", null);
            if (response.IsSuccessStatusCode)
            {
                Console.Clear();
                await Console.Out.WriteLineAsync($"\x1b[32mUser connected to the track successfully!\x1b[0m");
                await MenuAesthetics.EnterBackToMenuAsync();
            }
            else
            {
                Console.Clear();
                await Console.Out.WriteLineAsync($"\x1b[31mSomething went wrong! \nStatuscode: {response.StatusCode}\x1b[0m");
                await MenuAesthetics.EnterBackToMenuAsync();
            }
        }
    }
}
