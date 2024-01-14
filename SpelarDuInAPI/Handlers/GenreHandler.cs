using Microsoft.EntityFrameworkCore;
using SpelarDuInAPI.Services;
using SpelarDuInAPI.Models.DTO;
using SpelarDuInAPI.Models.ViewModels;

using static SpelarDuInAPI.Services.IDbHelper;

namespace SpelarDuInAPI.Handlers
{
    public class GenreHandler
    {
        // Hämta alla genre kopplad till en specifik person     Sean

        public static void ListUsersGenres(IDbHelper dbHelper, int userId)
        {
            dbHelper.ListUsersGenres(userId);

        }

        public static void AddNewGenre(IDbHelper dbHelper, GenreDto newGenre)
        {
            dbHelper.AddNewGenre(newGenre);          
        }
    }
}
