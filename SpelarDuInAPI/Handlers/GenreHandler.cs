using Microsoft.EntityFrameworkCore;

using SpelarDuInAPI.Models.DTO;
using SpelarDuInAPI.Models.ViewModels;
using SpelarDuInAPI.Services;
using System.Net;
using static SpelarDuInAPI.Services.IDbHelper;

namespace SpelarDuInAPI.Handlers
{
    public class GenreHandler
    {
        // Hämta alla genre kopplad till en specifik person     Sean

        public static IResult ListUsersGenres(IDbHelper dbHelper, int userId)
        {
            var allGenres = dbHelper.ListUsersGenres(userId);

            if (allGenres == null)
            {
                return Results.Conflict("No such user");
            }

            return Results.Json(allGenres);
        }

        public static IResult AddNewGenre(IDbHelper dbHelper, GenreDto newGenre)
        {
            dbHelper.AddNewGenre(newGenre);
            return Results.StatusCode((int)HttpStatusCode.Created);
        }
    }
}
