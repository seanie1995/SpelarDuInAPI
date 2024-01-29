using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SpelarDuInAPI.Data;
using SpelarDuInAPI.Models;
using SpelarDuInAPI.Models.DTO;
using SpelarDuInAPI.Models.ViewModels;
using SpelarDuInAPI.Services;
using System.Net;

namespace SpelarDuInAPI.Handlers
{
    public class ArtistHandler //--- Jing
    {
        //List all artists
        public static IResult ListAllArtists(IArtistDbHelper artistDbHelper)
        {
            ArtistListViewModel[] result = artistDbHelper.ListAllArtists();
            return Results.Json(result);
        }

        //List all artists linked to a specific user 
        public static IResult ListUsersArtists(IArtistDbHelper artistDbHelper, int userId)
        {
            ArtistListViewModel[] result = artistDbHelper.ListUsersArtists(userId);

            return Results.Json(result);
        }

        //Show a specific artist
        public static IResult ViewArtist(IArtistDbHelper artistDbHelper, string artistName) 
        { 
            ArtistViewModel result = artistDbHelper.ViewAnArtist(artistName);
            return Results.Json(result);
        }

        //Add an new artist
        public static void AddNewArtist(IArtistDbHelper artistDbHelper, ArtistDto artistDto)
        {
            artistDbHelper.AddNewArtist(artistDto);
        }
    }
}
