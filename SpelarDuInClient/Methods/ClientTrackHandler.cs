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
    internal class ClientTrackHandler
    {
        public static async Task AddtrackAsync(HttpClient client)
        {
            await Console.Out.WriteLineAsync("Enter new track name:");
            string trackName = Console.ReadLine();

            await Console.Out.WriteLineAsync("What artist does the track belong to:");
            string trackArtist = Console.ReadLine();

            await Console.Out.WriteLineAsync("What genre does the track belong to?:");
            string trackGenre = Console.ReadLine();

            TrackDto newTrack = new TrackDto()
            {
                TrackTitle = trackName,
                Artist = trackArtist,
                Genre = trackGenre
            };
            string json = JsonSerializer.Serialize(newTrack);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/track", content);

            if (!response.IsSuccessStatusCode)
            {
                await Console.Out.WriteLineAsync($"Failed to create track (statuscode {response.StatusCode})");
            }

            await Console.Out.WriteLineAsync("Press enter to go back to main menu");
        }

        public static async Task AddtrackConnectedToSingleUserAsync(HttpClient client)
        {
            await Console.Out.WriteLineAsync("Enter new track name:");
            string trackName = Console.ReadLine();

            await Console.Out.WriteLineAsync("What artist does the track belong to:");
            string trackArtist = Console.ReadLine();

            await Console.Out.WriteLineAsync("What genre does the track belong to?:");
            string trackGenre = Console.ReadLine();

            TrackDto newTrack = new TrackDto()
            {
                TrackTitle = trackName,
                Artist = trackArtist,
                Genre = trackGenre
            };
            string json = JsonSerializer.Serialize(newTrack);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/track", content);

            if (!response.IsSuccessStatusCode)
            {
                await Console.Out.WriteLineAsync($"Failed to create track (statuscode {response.StatusCode})");
            }

            await Console.Out.WriteLineAsync("Press enter to go back to main menu");
        }

        public static async Task GetAlltracksFromSingleUserAsync(HttpClient client, int userId)
        {
            //Calling API endpoint
            HttpResponseMessage response = await client.GetAsync($"/user/{userId}/track");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to list all tracks from user {response.StatusCode}");
            }

            string content = await response.Content.ReadAsStringAsync();

            TrackViewModel[] alltracksLinkedToUser = JsonSerializer.Deserialize<TrackViewModel[]>(content);
            foreach (var tracks in alltracksLinkedToUser)
            {
                await Console.Out.WriteLineAsync($"Id: {tracks.Id,-3}:\t Artist- {tracks.Artist}:\t Song- {tracks.TrackTitle,-30}");
            }
        }
    }
}
