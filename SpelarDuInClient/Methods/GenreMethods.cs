using SpelarDuInAPIClient.Models;
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
        public static async Task CreateNewGenreAsync(HttpClient client, int userId)
        {
            // Adding new genre into database

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

            await AutoConnectGenreAsync(client, userId, name);

            await Console.Out.WriteLineAsync("Press enter to go back to main menu");
            Console.ReadLine();
            
            
        }

        public static async Task ListUserGenresAsync(HttpClient client, int userId)
        {

            HttpResponseMessage response = await client.GetAsync($"/user/{userId}/genre"); // Anropar API endpoint som vi skapat i vår API.

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to list genres connected to user {response.StatusCode}");
            }

            string content = await response.Content.ReadAsStringAsync();

            GenreViewModel[] allGenres = JsonSerializer.Deserialize<GenreViewModel[]>(content); // Deserialize JSON object retrieved from API

            foreach (var genre in allGenres)
            {
                await Console.Out.WriteLineAsync($"{genre.Id}:\t{genre.GenreName}");
            }

        }

        public static async Task AutoConnectGenreAsync(HttpClient client, int userId, string name)
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
    }
}
