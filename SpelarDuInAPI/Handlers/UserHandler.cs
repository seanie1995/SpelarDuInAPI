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
            var users = dbHelper.ShowAllUsersAllInfo();
            return Results.Json(users);
        }
        public static IResult GetAllUsers(IUserDbHelper dbHelper)
        {
            UserViewModel[] result = dbHelper.GetAllUsers();
            return Results.Json(result);
        }

        public static IResult CreateUser(IUserDbHelper dbHelper, UserDto user)
        {
            dbHelper.CreateUser(user);
            return Results.StatusCode((int)HttpStatusCode.Created);
        }

        public static IResult ConnectUserToOneGenre(IUserDbHelper dbHelper, int userId, int genreId)
        {
            dbHelper.ConnectUserToOneGenre(userId, genreId);
            return Results.StatusCode((int)HttpStatusCode.OK);
        }

        public static IResult ConnectUserToOneArtist(IUserDbHelper dbHelper, int userId, int artistId)
        {
            dbHelper.ConnectUserToOneArtist(userId, artistId);
            return Results.StatusCode((int)HttpStatusCode.OK);
        }

        public static IResult ConnectUserToOneTrack(IUserDbHelper dbHelper, int userId, int trackId)
        {
            dbHelper.ConnectUserToOneTrack(userId, trackId);
            return Results.StatusCode((int)HttpStatusCode.OK);
        }
    }
}
