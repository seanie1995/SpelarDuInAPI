using SpelarDuInAPI.Models;
using SpelarDuInAPI.Data;
using SpelarDuInAPI.Models.DTO;
using System.Net;
using Microsoft.EntityFrameworkCore;
using SpelarDuInAPI.Models.ViewModels;
using System.Linq;
using SpelarDuInAPI.Services;

namespace SpelarDuInAPI.Handlers
{
    public class UserHandler
    {
        public static IResult ShowAllUsersAllInfo(IUserDbHelper dbHelper)
        {
            try
            {
                var users = dbHelper.ShowAllUsersAllInfo();
                return Results.Json(users);
            }
            catch (InvalidDataException ex)
            {
                return Results.Json(new { Error = "No user in database" });
            }
        }

        public static IResult GetAllUsers(IUserDbHelper dbHelper)
        {
            try
            {
                UserViewModel[] result = dbHelper.GetAllUsers();
                return Results.Json(result);
            }
            catch (InvalidDataException ex)
            {
                return Results.Json(new { Error = "No user in database" });
            }
        }

        public static IResult CreateUser(IUserDbHelper dbHelper, UserDto user)
        {
            try
            {
                dbHelper.CreateUser(user);
                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            catch (InvalidDataException ex)
            {
                return Results.BadRequest(new { Error = "You must enter a name!" });
            }
            catch (InvalidOperationException ex)
            {
                return Results.Conflict(new { Error = "This username already exists!" });
            }
        }

        public static IResult ConnectUserToOneGenre(IUserDbHelper dbHelper, int userId, int genreId)
        {
            try
            {
                dbHelper.ConnectUserToOneGenre(userId, genreId);
                return Results.StatusCode((int)HttpStatusCode.OK);
            }
            catch (InvalidDataException ex)
            {
                return Results.NotFound(new { Error = $"Person with id:{userId} doesnt exist!" });
            }
            catch (InvalidOperationException ex)
            {
                return Results.NotFound(new { Error = $"Genre with id:{genreId} doesnt exist!" });
            }
        }

        public static IResult ConnectUserToOneArtist(IUserDbHelper dbHelper, int userId, int artistId)
        {
            try
            {
                dbHelper.ConnectUserToOneArtist(userId, artistId);
                return Results.StatusCode((int)HttpStatusCode.OK);
            }
            catch (InvalidDataException ex)
            {
                return Results.NotFound(new { Error = $"Person with id:{userId} doesnt exist!" });
            }
            catch (InvalidOperationException ex)
            {
                return Results.NotFound(new { Error = $"Artist with id:{artistId} doesnt exist!" });
            }
        }

        public static IResult ConnectUserToOneTrack(IUserDbHelper dbHelper, int userId, int trackId)
        {
            try
            {
                dbHelper.ConnectUserToOneTrack(userId, trackId);
                return Results.StatusCode((int)HttpStatusCode.OK);
            }
            catch (InvalidDataException ex)
            {
                return Results.NotFound(new { Error = $"Person with id:{userId} doesnt exist!" });
            }
            catch (InvalidOperationException ex)
            {
                return Results.NotFound(new { Error = $"Track with id:{trackId} doesnt exist!" });
            }
        }
    }
}
