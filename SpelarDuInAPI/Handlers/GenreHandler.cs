using Microsoft.EntityFrameworkCore;
using SpelarDuInAPI.Services;
using SpelarDuInAPI.Models.DTO;
using SpelarDuInAPI.Models.ViewModels;

using static SpelarDuInAPI.Services.IGenreDbHelper;
using System.Net;

namespace SpelarDuInAPI.Handlers
{
    public class GenreHandler
    {
        // Hämta alla genre kopplad till en specifik person     Sean

        public static IResult ListUsersGenres(IGenreDbHelper dbHelper, int userId)
        {
            try
            {
                GenreViewModel[] result = dbHelper.ListUsersGenres(userId);

                return Results.Json(result);
            }
            catch 
            {
                return Results.Json(new { Error = "No genres in database" });
            }
            
        }

        public static IResult AddNewGenre(IGenreDbHelper dbHelper, GenreDto newGenre)
        {                    
            try
            {
                dbHelper.AddNewGenre(newGenre);
                
            }
            catch
            {
                return Results.StatusCode((int)HttpStatusCode.InternalServerError);
            }

            return Results.StatusCode((int)HttpStatusCode.Created);

            // Test comment
        }
    }   
}
