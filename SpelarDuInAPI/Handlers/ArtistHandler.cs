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
    public class ArtistHandler
    {
        //Get all artists linked to a specific person --- Jing
        public static IResult ListUsersArtists(IArtistDbHelper artistDbHelper, int userId)
        {
            ArtistViewModel[] result = artistDbHelper.ListUsersArtists(userId);

            return Results.Json(result);
        }

        //Add an new artist --- Jing
        public static void AddNewArtist(IArtistDbHelper artistDbHelper, ArtistDto artistDto)
        {
            artistDbHelper.AddNewArtist(artistDto);
        }
    }
}
