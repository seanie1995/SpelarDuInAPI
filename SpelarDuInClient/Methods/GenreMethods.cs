using SpelarDuInAPIClient.Models;
using SpelarDuInClient.Menu;
using SpelarDuInClient.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpelarDuInAPIClient.Methods
{
    public class GenreMethods
    {
        public static async Task CreateNewGenreAsync(HttpClient client, int userId, UserViewModel user)
        {
            // Adding new genre into database

            Console.Clear();

            await Console.Out.WriteLineAsync("Enter new genre name:");

            string name = Console.ReadLine();

            GenreDto newGenre = new GenreDto()
            {
                GenreName = name
            };

            string json = JsonSerializer.Serialize(newGenre);

            StringContent jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/genre", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                await Console.Out.WriteLineAsync($"Failed to create genre (status code {response.StatusCode})");
            }

            await ConnectGenreAsync(client, userId, name);

            await Console.Out.WriteLineAsync("Press enter to return to menu");

            Console.ReadLine();

            await GenreMenu.GenreMenuAsync(client, userId, user);


        }

        public static async Task ListUserGenresAsync(HttpClient client, int userId, UserViewModel user)
        {

            Console.Clear();

            HttpResponseMessage response = await client.GetAsync($"/user/{userId}/genre"); // Anropar API endpoint som vi skapat i vår API.

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to list users {response.StatusCode}");
            }

            string content = await response.Content.ReadAsStringAsync();

            GenreViewModel[] allGenres = JsonSerializer.Deserialize<GenreViewModel[]>(content); // Deserialize JSON object retrieved from API

            //await Console.Out.WriteLineAsync();
            //await Console.Out.WriteLineAsync();

            foreach (var genre in allGenres)
            {
                await Console.Out.WriteLineAsync($"{genre.Id}:\t{genre.GenreName}");
            }

            await Console.Out.WriteLineAsync("Press enter to continue:");

            Console.ReadKey();

            await GenreMenu.GenreMenuAsync(client, userId, user);

        }

        public static async Task ConnectGenreAsync(HttpClient client, int userId, string name)
        {

            // Finding created genre within database to connect with user            

            HttpResponseMessage response = await client.GetAsync($"/genre");

            string content = await response.Content.ReadAsStringAsync();

            GenreViewModel[] allGenres = JsonSerializer.Deserialize<GenreViewModel[]>(content);

            await Console.Out.WriteLineAsync($"{allGenres.Length}");

            GenreViewModel newGenre = allGenres
                .Where(i => i.GenreName == name)
                .FirstOrDefault();

            int newGenreId = newGenre.Id;

            if (newGenreId == 0)
            {
                await Console.Out.WriteLineAsync($"Failed to find the genre with name '{name}' in the list.");
                return;
            }

            // Using method to connect new genre to user

            HttpResponseMessage response2 = await client.PostAsync($"/user/{userId}/genre/{newGenreId}", null);

            if (response2.IsSuccessStatusCode)
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

        public static async Task ListAllGenresAsync(HttpClient client, int userId, UserViewModel user)
        {
            await Task.Run(() => Console.Clear());
            HttpResponseMessage response = await client.GetAsync("/genre");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to list artists {response.StatusCode}");
            }
            string content = await response.Content.ReadAsStringAsync();
            //pack up to a list of artists
            GenreViewModel[] genres = JsonSerializer.Deserialize<GenreViewModel[]>(content);
            // read through the list
            foreach (var a in genres)
            {
                await Console.Out.WriteLineAsync($"{a.Id}:\t{a.GenreName}");
            }
            await Console.Out.WriteLineAsync("Press enter to go continue");
            Console.ReadLine();

            await GenreMenu.GenreMenuAsync(client, userId, user);
        }
    }
}
