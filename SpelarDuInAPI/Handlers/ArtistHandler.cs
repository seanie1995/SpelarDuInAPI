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
            try
            {
                ArtistListViewModel[] result = artistDbHelper.ListAllArtists();
                return Results.Json(result);
            }
            catch
            {
                return Results.Json(new { Error = "No artists in database" });
            }
        }

        //List all artists linked to a specific user 
        public static IResult ListUsersArtists(IArtistDbHelper artistDbHelper, int userId)
        {
            try
            {
                ArtistListViewModel[] result = artistDbHelper.ListUsersArtists(userId);
                return Results.Json(result);
            }
            catch
            {
                return Results.Json(new { Error = "No such user's artists in database" });
            }
        }

        //Show a specific artist
        public static IResult ViewArtist(IArtistDbHelper artistDbHelper, int artistId)
        {
            try
            {
                ArtistViewModel result = artistDbHelper.ViewAnArtist(artistId);
                return Results.Json(result);
            }
            catch
            {
                return Results.Json(new { Error = "No such artist in database" });
            }
        }

        //Add an new artist
        public static IResult AddNewArtist(IArtistDbHelper artistDbHelper, ArtistDto artistDto)
        {
            try
            {
                artistDbHelper.AddNewArtist(artistDto);
            }
            catch
            {
                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }
            return Results.StatusCode((int)HttpStatusCode.Created);
        }
    }
}
