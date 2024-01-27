using SpelarDuInAPIClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpelarDuInAPIClient.Methods
{
    public class GenreMethods
    {
        public static async Task AddGenreAsync(HttpClient client)
        {
            await Console.Out.WriteLineAsync("Enter new genre name:");

            string? name = Console.ReadLine();

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
    }
}
