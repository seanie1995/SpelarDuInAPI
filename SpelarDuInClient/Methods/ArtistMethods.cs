using SpelarDuInClient.Models;
using SpelarDuInClient.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpelarDuInClient.Methods
{
    public class ArtistMethods
    {
        public static async Task AddNewArtistAysnc(HttpClient client)
        {
            Console.Clear();
            await Console.Out.WriteLineAsync("Enter artist name: ");
            string artistName = Console.ReadLine();
            await Console.Out.WriteLineAsync("Enter description: ");
            string description = Console.ReadLine();

            ArtistDto artist = new ArtistDto()
            {
                ArtistName = artistName,
                Description = description
            };

            string json = JsonSerializer.Serialize(artist);
            StringContent jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/artist", jsonContent);
            if (!response.IsSuccessStatusCode)
            {
                await Console.Out.WriteLineAsync($"Failed to add new artist(status code: {response.StatusCode} )");
            }
            await Console.Out.WriteLineAsync("Press enter to go back to main menu");
            Console.ReadLine();
            Console.Clear();
        }

        public static async Task ListUserArtistsAsync(HttpClient client, int userId)
        {
            //Console.Clear();
            HttpResponseMessage response = await client.GetAsync($"/user/{userId}/artist");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to list artists {response.StatusCode}");
            }
            string content = await response.Content.ReadAsStringAsync();
            //pack up to a list of artists
            ArtistListViewModel[] artists = JsonSerializer.Deserialize<ArtistListViewModel[]>(content);
            // read through the list
            foreach (var arts in artists)
            {
                await Console.Out.WriteLineAsync($"{arts.Id}:\t{arts.ArtistName}");
            }
            await Console.Out.WriteLineAsync("Press enter to go back to main menu");
            Console.ReadLine();
            Console.Clear();
        }

        public static async Task ListAllArtistsAsync(HttpClient client)
        {
            //Console.Clear();
            HttpResponseMessage response = await client.GetAsync("/artist");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to list artists {response.StatusCode}");
            }
            string content = await response.Content.ReadAsStringAsync();
            //pack up to a list of artists
            ArtistListViewModel[] artists = JsonSerializer.Deserialize<ArtistListViewModel[]>(content);
            // read through the list
            foreach (var a in artists)
            {
                await Console.Out.WriteLineAsync($"{a.Id}:\t{a.ArtistName}");
            }
            await Console.Out.WriteLineAsync("Press enter to go back to main menu");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
