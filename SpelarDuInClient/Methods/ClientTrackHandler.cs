﻿using SpelarDuInClient.Menu;
using SpelarDuInClient.Models.DTO;
using SpelarDuInClient.Models.ViewModels;
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
        public static async Task AddtrackAsync(HttpClient client, int userId)
        {
            Console.Clear();
            Console.CursorVisible = true;
            await Console.Out.WriteLineAsync("Adding new track");
            await MenuAesthetics.UnderLineHeaderAsync();
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

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json" );

            var response = await client.PostAsync("/track", content);

            if (!response.IsSuccessStatusCode)
            {
                await Console.Out.WriteLineAsync($"Failed to create track (statuscode {response.StatusCode})");
            }

            await AutoAddingtrackToSingleUserAsync(client, userId, trackName);

            await MenuAesthetics.EnterBackToMenuAsync();

        }

        public static async Task AutoAddingtrackToSingleUserAsync(HttpClient client, int userId, string trackName)
        {
            //Finding track 
            HttpResponseMessage response = await client.GetAsync("/track");

            string content = await response.Content.ReadAsStringAsync();

            TrackViewModel[] allTracks = JsonSerializer.Deserialize<TrackViewModel[]>(content);

            TrackViewModel newtrack = allTracks
                .Where(i => i.TrackTitle == trackName)
                .FirstOrDefault();

            int newTrackId = newtrack.Id;

            if (newTrackId == 0)
            {
                await Console.Out.WriteLineAsync($"Failed to find the track with name '{trackName}' in the database.");
            }
            //Connecting track to user 
            HttpResponseMessage connectUserToTrack = await client.PostAsync($"/user/{userId}/track/{newTrackId}", null);

            if (connectUserToTrack.IsSuccessStatusCode)
            {
                Console.Clear();
                await Console.Out.WriteLineAsync("\x1b[32mUser connected to track succefully!\x1b[0m");
            }
            else
            {
                Console.Clear();
                await Console.Out.WriteLineAsync($"\x1b[31mFailed to connect. Statuscode: {response.StatusCode}\x1b[0m");
            }

        }

        public static async Task GetAlltracksFromSingleUserAsync(HttpClient client, int userId)
        {
            await Task.Run(() => Console.Clear());
            
            //Calling API endpoint
            HttpResponseMessage response = await client.GetAsync($"/user/{userId}/track");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to list all tracks from user {response.StatusCode}");
            }

            string content = await response.Content.ReadAsStringAsync();

            TrackViewModel[] alltracksLinkedToUser = JsonSerializer.Deserialize<TrackViewModel[]>(content);
            string formatedTitles = string.Format("{0, -5} : {1,-30} : {2}", "Id:", "Track title:", "Artist");
            Console.ForegroundColor = ConsoleColor.White;
            await Console.Out.WriteLineAsync(formatedTitles);
            await Console.Out.WriteLineAsync("________________________________________________________________________________");
            foreach (var tracks in alltracksLinkedToUser)
            {
                Console.ForegroundColor= ConsoleColor.DarkYellow;
                string formattedOutput = String.Format("{0, -5} : {1,-30} : {2}", tracks.Id, tracks.TrackTitle, tracks.Artist);
                await Console.Out.WriteLineAsync(formattedOutput);
                Console.ResetColor();
            }

            await MenuAesthetics.EnterBackToMenuAsync();
        }
    }
}
