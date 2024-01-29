using Microsoft.EntityFrameworkCore;
using SpelarDuInAPI.Data;
using SpelarDuInAPI.Models;
using SpelarDuInAPI.Models.DTO;
using SpelarDuInAPI.Models.ViewModels;
using System.Linq.Expressions;

namespace SpelarDuInAPI.Services
{
    public interface IArtistDbHelper                      // ---- Jing
    {
        void AddNewArtist(ArtistDto newArtist);
        ArtistListViewModel[] ListAllArtists();
        ArtistListViewModel[] ListUsersArtists(int userId);
        ArtistViewModel ViewAnArtist(int artistId);
    }

    public class ArtistDbHelper : IArtistDbHelper
    {
        //Dependency injection here
        private ApplicationContext _context;
        public ArtistDbHelper(ApplicationContext context)
        {
            _context = context;
        }
        public void AddNewArtist(ArtistDto artistDto)
        {
            //vertify if the new artist's name has exists or empty first
            var allArtists = 
                _context.Artists
                .ToArray();
            if (allArtists.Any(a => a.ArtistName == artistDto.ArtistName))
            {
                throw new InvalidOperationException($"{artistDto.ArtistName} has exists.");
            }
            else if (string.IsNullOrEmpty(artistDto.ArtistName))
            {
                throw new InvalidOperationException("New artist's name must not be empty.");
            }
            //then add an new artist
            else
            {
                _context.Artists.Add(new Artist()
                {
                    ArtistName = artistDto.ArtistName,
                    Description = artistDto.Description
                });
                _context.SaveChanges();
            }
        }

        //List all artists in the database
        public ArtistListViewModel[] ListAllArtists()
        {
            var result = _context.Artists
                .Select(a => new ArtistListViewModel()
                {
                    Id = a.Id,
                    ArtistName = a.ArtistName,
                    Description= a.Description,
                }).ToArray();
            return result;
        }

        //List all artists linked to a specific user 
        public ArtistListViewModel[] ListUsersArtists(int userId)
        {
            //find the speicific user
            User? user =
                _context.Users
                .Include(a => a.Artists)
                .Where(a => a.Id == userId)
                .SingleOrDefault();

            if (user == null)
            {
                throw new InvalidOperationException("User has not found.");
            }

            // show all artists

            ArtistListViewModel[] result = user.Artists
                .Select(r => new ArtistListViewModel
                {
                    Id = r.Id,
                    ArtistName = r.ArtistName,
                    Description = r.Description
                }).ToArray();
            return result;
        }

        //Show a specific artist
        public ArtistViewModel ViewAnArtist(int artistId)
        {
            //find the specific artist with name
            Artist? artist = _context.Artists
                .Where(a => a.Id == artistId)
                .Include(a => a.Tracks)
                .SingleOrDefault();

            if (artist == null)
            {
                throw new InvalidOperationException("No such artist with the Id!");
            }
            ArtistViewModel result = new ArtistViewModel()
            {
                ArtistName = artist.ArtistName,
                Description = artist.Description,
                Tracks = artist.Tracks
                    .Select(t => new TrackViewModel()
                    {
                        Id = t.Id,
                        TrackTitle = t.TrackTitle,
                    }).ToArray()
            };
            return result;
        }
    }
}
