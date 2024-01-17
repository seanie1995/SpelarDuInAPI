using Microsoft.EntityFrameworkCore;
using SpelarDuInAPI.Services;
using SpelarDuInAPI.Models.DTO;
using SpelarDuInAPI.Models.ViewModels;

using static SpelarDuInAPI.Services.IGenreDbHelper;

namespace SpelarDuInAPI.Handlers
{
    public class GenreHandler
    {
        // Hämta alla genre kopplad till en specifik person     Sean

        public static IResult ListUsersGenres(IGenreDbHelper dbHelper, int userId)
        {
            GenreViewModel[] result = dbHelper.ListUsersGenres(userId);

            return Results.Json(result);
        }

        public static void AddNewGenre(IGenreDbHelper dbHelper, GenreDto newGenre)
        {
            dbHelper.AddNewGenre(newGenre);          
        }
    }
}
