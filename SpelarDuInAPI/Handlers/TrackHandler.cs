using Microsoft.EntityFrameworkCore;
using SpelarDuInAPI.Data;
using SpelarDuInAPI.Models;
using SpelarDuInAPI.Models.DTO;
using SpelarDuInAPI.Models.ViewModels;
using System.Net;

namespace SpelarDuInAPI.Handlers
{
    public class TrackHandler
    {
        
        public static IResult AddNewTrack(ApplicationContext context, TrackDto trackDto)
        {
            //Find or create genre
            var genre = context.Genres
                .FirstOrDefault(g => g.GenreName == trackDto.Genre);
            if (genre == null)
            {
                genre = new Genre
                {
                    GenreName = trackDto.Genre
                };
                context.Genres.Add(genre);
            }
            //Find or create artist
            var artist = context.Artists
                .FirstOrDefault(a => a.ArtistName == trackDto.Artist);
            if(artist == null)
            {
                artist = new Artist
                {
                    ArtistName = trackDto.Artist
                };
                context.Artists.Add(artist);
            }
            //create new track 
            var newTrack = new Track
            {
                TrackTitle = trackDto.TrackTitle,
                Artist = artist,
                Genre = genre,
            };
            context.Tracks.Add(newTrack);
            context.SaveChanges();

            return Results.StatusCode((int)HttpStatusCode.Created);
        }
        public static IResult GetAllTracksFromSingleUser(ApplicationContext context, int userId)
        {   
            //Find user
            var user = context.Users
                .Include(u => u.Tracks)
                .FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return Results.NotFound("User not found");
            }
            //Show all tracktitles
            var result = user.Tracks
                .Select(r => new TrackViewModel
                {
                    TrackTitle = r.TrackTitle
                }).ToArray();
            return Results.Json(result);
        }
    }
}
