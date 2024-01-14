using Microsoft.EntityFrameworkCore;
using SpelarDuInAPI.Data;
using SpelarDuInAPI.Models;
using SpelarDuInAPI.Models.DTO;
using SpelarDuInAPI.Models.ViewModels;
using System.Net;

namespace SpelarDuInAPI.Handlers
{
    public class GenreHandler
    {
        // Hämta alla genre kopplad till en specifik person     Sean

        public static IResult ListUsersGenres(ApplicationContext context, int userId)
        {
            GenreViewModel[] result = 
                context.Genres
                .Include(x => x.Users)
                .Where(x => x.Users.Any(x => x.Id == userId))
                .Select(x => new GenreViewModel()
                {
                    GenreName = x.GenreName,
                }).ToArray();

            if (result == null)
            {
                return Results.NotFound();
            }

            return Results.Json(result);

        }

        public static IResult AddNewGenre(ApplicationContext context, GenreDto newGenre)
        {
            var allGenres = context.Genres
                .ToArray();

            if (allGenres.Any(x => x.GenreName == newGenre.GenreName))
            {
                return Results.Conflict($"{newGenre.GenreName} already exists.");
            }

            if (string.IsNullOrEmpty(newGenre.GenreName))
            {
                return Results.Conflict($"New genre name must not be empty.");
            }

            var genre = new Genre
            {
                GenreName = newGenre.GenreName
            };

            allGenres.

            context.SaveChanges();

            return Results.StatusCode((int)HttpStatusCode.Created);
             
        }
    }
}
