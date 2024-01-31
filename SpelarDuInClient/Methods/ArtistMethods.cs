using SpelarDuInClient.Menu;
using SpelarDuInClient.Models.DTO;
using SpelarDuInClient.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpelarDuInClient.Methods
{
    public class ArtistMethods
    {
        public static async Task AddNewArtistAysnc(HttpClient client, UserViewModel user)
        {
            Console.Clear();
            Console.CursorVisible = true;
            await Console.Out.WriteLineAsync("Adding new artist:");
            await MenuAesthetics.UnderLineHeaderAsync();
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
            await ConnectArtistAsync(client, artistName, user);
            await Console.Out.WriteLineAsync("Press enter to go back to main menu");
            Console.ReadLine();
            await ArtistMenu.ArtistMenuAsync(client, user);
        }

        public static async Task ListUserArtistsAsync(HttpClient client, UserViewModel user)
        {
            Console.Clear();
            HttpResponseMessage response = await client.GetAsync($"/user/{user.Id}/artist");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to list artists {response.StatusCode}");
            }
            string content = await response.Content.ReadAsStringAsync();
            //pack up to a list of artists
            ArtistListViewModel[] artists = JsonSerializer.Deserialize<ArtistListViewModel[]>(content);
            // read through the list
            Console.CursorVisible = true;
            await Console.Out.WriteLineAsync($"{user.UserName}s favorite artists:");
            await MenuAesthetics.UnderLineHeaderAsync();
            foreach (var arts in artists)
            {
                await Console.Out.WriteLineAsync($"{arts.Id}:\t{arts.ArtistName}");
            }
            await MenuAesthetics.EnterBackToMenuAsync();
            await ArtistMenu.ArtistMenuAsync(client, userId, user);
        }

        public static async Task ListAllArtistsAsync(HttpClient client, UserViewModel user)
        {
            Console.Clear();
            HttpResponseMessage response = await client.GetAsync("/artist");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to list artists {response.StatusCode}");
            }
            string content = await response.Content.ReadAsStringAsync();
            //pack up to a list of artists
            ArtistListViewModel[] artists = JsonSerializer.Deserialize<ArtistListViewModel[]>(content);
            // read through the list
            Console.CursorVisible = true;
            await Console.Out.WriteLineAsync("All artist:");
            await MenuAesthetics.UnderLineHeaderAsync();
            foreach (var a in artists)
            {
                await Console.Out.WriteLineAsync($"{a.Id}:\t{a.ArtistName}");
            }
            await MenuAesthetics.EnterBackToMenuAsync();
            await ArtistMenu.ArtistMenuAsync(client, userId, user);
        }

        public static async Task ViewAnArtistAsync(HttpClient client, string artistName, UserViewModel user)
        {
            Console.Clear();
            HttpResponseMessage response = await client.GetAsync($"/artist/{artistName}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error fetching artist {response.StatusCode}");
            }
            string content = await response.Content.ReadAsStringAsync();
            ArtistViewModel2 artist = JsonSerializer.Deserialize<ArtistViewModel2>(content);

            await Console.Out.WriteLineAsync(artist.ArtistName);
            await Console.Out.WriteLineAsync(artist.Description);

            Console.ReadLine();
            await ArtistMenu.ArtistMenuAsync(client, user);
            //not finish yet here
        }

        public static async Task ConnectArtistAsync(HttpClient client, string artistName, UserViewModel user)
        {

            // Finding created artist within database to connect with user            

            HttpResponseMessage response = await client.GetAsync($"/artist");

            string content = await response.Content.ReadAsStringAsync();

            ArtistListViewModel[] allArtists = JsonSerializer.Deserialize<ArtistListViewModel[]>(content);

            await Console.Out.WriteLineAsync($"{allArtists.Length}");

            ArtistListViewModel newArtist = allArtists
                .Where(i => i.ArtistName == artistName)
                .FirstOrDefault();

            int newArtistId = newArtist.Id;

            if (newArtistId == 0)
            {
                await Console.Out.WriteLineAsync($"Failed to find the artist with name '{artistName}' in the list.");
                return;
            }

            // Using method to connect new artist to user

            HttpResponseMessage response2 = await client.PostAsync($"/user/{user.Id}/artist/{newArtistId}", null);

            if (response2.IsSuccessStatusCode)
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
    }
}
