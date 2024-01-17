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

        public static void ListUsersGenres(IGenreDbHelper dbHelper, int userId)
        {
            dbHelper.ListUsersGenres(userId);

        }

        public static void AddNewGenre(IGenreDbHelper dbHelper, GenreDto newGenre)
        {
            dbHelper.AddNewGenre(newGenre);          
        }
    }
}
