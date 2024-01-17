using SpelarDuInAPI.Data;
using SpelarDuInAPI.Models.DTO;
using SpelarDuInAPI.Models.ViewModels;
using SpelarDuInAPI.Models;
using System.Net;
using SpelarDuInAPI.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace SpelarDuInAPI.Services
{
    public interface IUserDbHelper
    {
        List<UserViewModelAllInfo> ShowAllUsersAllInfo();
        UserViewModel[] GetAllUsers();
        void CreateUser(UserDto user);
        void ConnectUserToOneGenre(int userId, int genreId);
        void ConnectUserToOneArtist(int userId, int artistId);
        void ConnectUserToOneTrack(int userId, int trackId);
    }
    public class UserDbHelper : IUserDbHelper
    {
        private ApplicationContext _context;
        public UserDbHelper(ApplicationContext context)
        {
            _context = context;
        }

        public void ConnectUserToOneGenre(int userId, int genreId)
        {
            User? user = _context.Users.Where(p => p.Id == userId).Include(p => p.Genres).SingleOrDefault();
            if (user == null)
            {
                Console.WriteLine($"Person with id:{userId} not found!");
            }
            Genre? genre = _context.Genres.SingleOrDefault(g => g.Id == genreId);
            if (genre == null)
            {
                Console.WriteLine($"Genre with id:{genreId} not found!");
            }
            user.Genres.Add(genre);
            _context.SaveChanges();
        }

        public void ConnectUserToOneArtist(int userId, int artistId)
        {
            User? user = _context.Users.Where(p => p.Id == userId).Include(p => p.Artists).SingleOrDefault();
            if (user == null)
            {
                Console.WriteLine($"Person with id:{userId} not found!");
            }
            Artist? artist = _context.Artists.SingleOrDefault(g => g.Id == artistId);
            if (artist == null)
            {
                Console.WriteLine($"Genre with id:{artistId} not found!");
            }
            user.Artists.Add(artist);
            _context.SaveChanges();
        }

        public void ConnectUserToOneTrack(int userId, int trackId)
        {
            User? user = _context.Users.Where(p => p.Id == userId).Include(p => p.Tracks).SingleOrDefault();
            if (user == null)
            {
                Console.WriteLine($"Person with id:{userId} not found!");
            }
            Track? track = _context.Tracks.SingleOrDefault(g => g.Id == trackId);
            if (track == null)
            {
                Console.WriteLine($"Genre with id:{trackId} not found!");
            }
            user.Tracks.Add(track);
            _context.SaveChanges();
        }

        public void CreateUser(UserDto user)
        {
            try
            {
                _context.Users.Add(new User
                {
                    UserName = user.UserName
                });
                if (string.IsNullOrEmpty(user.UserName))
                {
                    Results.Json("You must enter a name!");
                    throw new InvalidDataException("You must enter a name!");
                }
                if (_context.Users.Any(u => u.UserName == user.UserName))
                {
                    Results.Json("This username already exists!");
                    throw new InvalidOperationException("This username already exists!");
                }
                _context.SaveChanges();
            }
            catch(InvalidDataException ex)
            {
                Results.Json("You must enter a name!");
                throw;
            }
            catch(InvalidOperationException ex)
            {
                Results.Json("This username already exists!");
                throw;
            }
        }

        public UserViewModel[] GetAllUsers()
        {
            UserViewModel[] result = _context.Users.Select(u => new UserViewModel { Id = u.Id, UserName = u.UserName }).ToArray();
            return result;
        }

        public List<UserViewModelAllInfo> ShowAllUsersAllInfo()
        {
            User[] user = _context.Users
               .Include(u => u.Artists)
               .Include(u => u.Tracks)
               .Include(u => u.Genres).ToArray();
            if (user == null)
            {
                Console.WriteLine("No user in database");
            }
            var userView = user.Select(u => new UserViewModelAllInfo()
            {
                UserName = u.UserName,
                Genres = u.Genres.Select(g => new GenreViewModel { GenreName = g.GenreName }).ToList(),
                Artists = u.Artists.Select(a => new ArtistViewModel { ArtistName = a.ArtistName, Description = a.Description }).ToList(),
                Tracks = u.Tracks.Select(t => new TrackViewModel { TrackTitle = t.TrackTitle }).ToList()
            }).ToList();
            return userView;
        }
    }
}
