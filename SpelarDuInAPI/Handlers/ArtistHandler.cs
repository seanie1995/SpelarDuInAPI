using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SpelarDuInAPI.Data;
using SpelarDuInAPI.Models;
using SpelarDuInAPI.Models.DTO;
using SpelarDuInAPI.Models.ViewModels;
using System.Net;

namespace SpelarDuInAPI.Handlers
{
    public class ArtistHandler
    {
        //Get all artists linked to a specific person --- Jing
        public static IResult ListUsersArtists(ApplicationContext context, int userId)
        {
            //find the speicific user
            User? user = 
                context.Users
                .Where(a => a.Id == userId)
                .Include(a => a.Artists)
                .SingleOrDefault();

            if (user == null)
            {
                return Results.NotFound("User has not found.");
            }

        // show all artists

            ArtistViewModel[] result = user.Artists
                .Select(r => new ArtistViewModel
                {
                    Id = r.Id,                      //need to show this Id? 
                    ArtistName = r.ArtistName,
                    Description = r.Description
                }).ToArray();

            return Results.Json(result);
                
        }

        //Add an new artist --- Jing
        public static IResult AddNewArtist(ApplicationContext context, ArtistDto artistDto)
        {
            //vertify if the new artist's name has exists or empty first
            var allArtists =
                context.Artists
                .ToArray();

            if (allArtists.Any(a => a.ArtistName == artistDto.ArtistName))
            {
                return Results.Conflict($"{artistDto.ArtistName} has exists.");
            }
            else if (string.IsNullOrEmpty(artistDto.ArtistName))
            {
                return Results.Conflict("New artist's name must not be empty.");
            }
            //then add an new artist
            else
            {
                context.Artists.Add(new Artist()
                {
                    ArtistName = artistDto.ArtistName,
                    Description = artistDto.Description
                });
                context.SaveChanges();
                return Results.StatusCode((int)HttpStatusCode.Created);
            }
        }
    }
}
