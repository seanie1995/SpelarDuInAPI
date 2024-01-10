using SpelarDuInAPI.Models;
using SpelarDuInAPI.Data;
using SpelarDuInAPI.Models.DTO;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace SpelarDuInAPI.Handlers
{
    public class UserHandler
    {
        public static IResult GetAllUsers(ApplicationContext context)
        {
            User? user = context.Users.SingleOrDefault();
            if (user != null)
            {
                return Results.Json(context.Users.Select(p => new { p.Id, p.UserName }).ToArray());
            }
            return Results.Json("No user in database");
        }

        public static IResult CreateUser(ApplicationContext context, UserDto user)
        {
            context.Users.Add(new User
            {
                UserName = user.UserName
            });
            context.SaveChanges();
            return Results.StatusCode((int)HttpStatusCode.Created);
        }

        public static IResult ConnectPersonToAGenre(ApplicationContext context, int userId, int genreId)
        {
            User? user = context.Users.Where(p => p.Id == userId).Include(p=>p.Genres).SingleOrDefault();
            if(user == null)
            {
                return Results.NotFound($"Person with id:{userId} not found!");
            }
            Genre? genre = context.Genres.SingleOrDefault(g => g.Id == genreId);
            if(genre == null)
            {
                return Results.NotFound($"Genre with id:{genreId} not found!");
            }
            user.Genres.Add(genre);
            context.SaveChanges();
            return Results.StatusCode((int)HttpStatusCode.OK);
        }

        public static IResult ConnectPersonToOneArtist(ApplicationContext context, int userId, int artistId)
        {
            User? user = context.Users.Where(p => p.Id == userId).Include(p => p.Artists).SingleOrDefault();
            if (user == null)
            {
                return Results.NotFound($"Person with id:{userId} not found!");
            }
            Artist? artist = context.Artists.SingleOrDefault(g => g.Id == artistId);
            if (artist == null)
            {
                return Results.NotFound($"Genre with id:{artistId} not found!");
            }
            user.Artists.Add(artist);
            context.SaveChanges();
            return Results.StatusCode((int)HttpStatusCode.OK);
        }
    }
}
