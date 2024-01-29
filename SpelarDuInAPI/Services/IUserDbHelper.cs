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
        public List<UserViewModelAllInfo> ShowAllUsersAllInfoOneUser(int userId);
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

        //----------------------------------------Methods
        public List<UserViewModelAllInfo> ShowAllUsersAllInfoOneUser(int userId)
        {
            //fetching the user
            User[] user = _context.Users.Where(u=>u.Id == userId)
               .Include(u => u.Artists)
               .Include(u => u.Tracks)
               .Include(u => u.Genres).ToArray();
            //using .Length because with .ToArray it'll never be null.
            if (user.Length == 0)
            {
                throw new InvalidDataException();
            }
            //Showing the info of the user we fetched earlier with viewModel
            var userView2 = user.Select(u => new UserViewModelAllInfo()
            {
                UserName = u.UserName,
                Genres = u.Genres.Select(g => new GenreViewModel { GenreName = g.GenreName }).ToList(),
                Artists = u.Artists.Select(a => new ArtistViewModel { ArtistName = a.ArtistName, Description = a.Description }).ToList(),
                Tracks = u.Tracks.Select(t => new TrackViewModel { TrackTitle = t.TrackTitle }).ToList()
            }).ToList();
            return userView2;
        }
        public List<UserViewModelAllInfo> ShowAllUsersAllInfo()
        {   //fetching the user
            User[] user = _context.Users
               .Include(u => u.Artists)
               .Include(u => u.Tracks)
               .Include(u => u.Genres).ToArray();
            //using .Length because with .ToArray it'll never be null.
            if (user.Length == 0)
            {
                throw new InvalidDataException();
            }
            //Showing the info of the user we fetched earlier with viewModel
            var userView = user.Select(u => new UserViewModelAllInfo()
            {
                UserName = u.UserName,
                Genres = u.Genres.Select(g => new GenreViewModel { GenreName = g.GenreName }).ToList(),
                Artists = u.Artists.Select(a => new ArtistListViewModel { ArtistName = a.ArtistName }).ToList(),
                Tracks = u.Tracks.Select(t => new TrackViewModel { TrackTitle = t.TrackTitle }).ToList()
            }).ToList();
            return userView;
        }

        public UserViewModel[] GetAllUsers()
        {
            UserViewModel[] result = _context.Users.Select(u => new UserViewModel { Id = u.Id, UserName = u.UserName }).ToArray();
            if (result.Length == 0) { throw new InvalidDataException(); }
            return result;
        }

        public void CreateUser(UserDto user)
        {
                _context.Users.Add(new User
                {
                    UserName = user.UserName
                });
                if (string.IsNullOrEmpty(user.UserName))
                {
                    throw new InvalidDataException();
                }
                if (_context.Users.Any(u => u.UserName == user.UserName))
                {
                    throw new InvalidOperationException();
                }
                _context.SaveChanges();
        }

        public void ConnectUserToOneGenre(int userId, int genreId)
        {
            User? user = _context.Users.Where(p => p.Id == userId).Include(p => p.Genres).SingleOrDefault();
            if (user == null)
            {
                throw new InvalidDataException();
            }
            Genre? genre = _context.Genres.SingleOrDefault(g => g.Id == genreId);
            if (genre == null)
            {
                throw new InvalidOperationException();
            }
            user.Genres.Add(genre);
            _context.SaveChanges();
        }

        public void ConnectUserToOneArtist(int userId, int artistId)
        {
            User? user = _context.Users.Where(p => p.Id == userId).Include(p => p.Artists).SingleOrDefault();
            if (user == null)
            {
                throw new InvalidDataException();
            }
            Artist? artist = _context.Artists.SingleOrDefault(g => g.Id == artistId);
            if (artist == null)
            {
                throw new InvalidOperationException();
            }
            user.Artists.Add(artist);
            _context.SaveChanges();
        }

        public void ConnectUserToOneTrack(int userId, int trackId)
        {
            User? user = _context.Users.Where(p => p.Id == userId).Include(p => p.Tracks).SingleOrDefault();
            if (user == null)
            {
                throw new InvalidDataException();
            }
            Track? track = _context.Tracks.SingleOrDefault(g => g.Id == trackId);
            if (track == null)
            {
                throw new InvalidOperationException();
            }
            user.Tracks.Add(track);
            _context.SaveChanges();
        }
    }
}
