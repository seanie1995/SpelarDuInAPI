using SpelarDuInClient.Menu;
using SpelarDuInClient.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpelarDuInClient.Methods
{
    internal class DiscographyMethods
    {
        public static async Task ListAlbumsAsync(HttpClient client, int userId, UserViewModel user)
        {
            Console.Clear();
            Console.CursorVisible = true;
            await Console.Out.WriteLineAsync($"Search for artist albums:");
            await MenuAesthetics.UnderLineHeaderAsync();
            await Console.Out.WriteLineAsync("Enter band name:");

            string bandName = Console.ReadLine();

            HttpResponseMessage response = await client.GetAsync($"/{bandName}/albums");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to get all albums {response.StatusCode}");
            }

            string content = await response.Content.ReadAsStringAsync();

            DiscographyViewModel discography = JsonSerializer.Deserialize<DiscographyViewModel>(content);

            await Task.Run(() => Console.Clear());
            await Console.Out.WriteLineAsync($"{bandName}s albums");
            await MenuAesthetics.UnderLineHeaderAsync();
            foreach (var album in discography.Album)
            {
                Console.WriteLine($"{album.Name}: {album.YearReleased}");
            }

            await MenuAesthetics.EnterBackToMenuAsync();
        }
    }
}
