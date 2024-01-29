using Microsoft.EntityFrameworkCore;
using SpelarDuInAPI.Data;
using SpelarDuInAPI.Models;
using SpelarDuInAPI.Models.DTO;
using SpelarDuInAPI.Models.ViewModels;
using System.Net;


namespace SpelarDuInAPI.Services
{    
    public interface IGenreDbHelper
    {
        void AddNewGenre(GenreDto newGenre);
        GenreViewModel[] ListUsersGenres(int userId);
    }

    public class GenreDbHelper : IGenreDbHelper
    {
        private ApplicationContext _context;    
            
        public GenreDbHelper(ApplicationContext context)
        {
            _context = context;
        }
            
        public void AddNewGenre(GenreDto newGenre)

        {
            var allGenres = _context.Genres
            .ToArray();

            if (allGenres.Any(x => x.GenreName == newGenre.GenreName))
            {
                throw new InvalidOperationException("Genre already exists");
            }

            if (string.IsNullOrEmpty(newGenre.GenreName))
            {
                throw new InvalidOperationException("User entered empty field");
            }
               
            var genre = new Genre
            {                    
                GenreName = newGenre.GenreName
            };  

            _context.Genres.Add(genre);

            _context.SaveChanges();               
        }
        public GenreViewModel[] ListUsersGenres(int userId) 
        {

            User? user =
                _context.Users
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            if (user == null)
            {
                throw new InvalidOperationException("No such user");
            }

            GenreViewModel[] result =
                _context.Genres
                .Include(x => x.Users)
                .Where(x => x.Users.Any(x => x.Id == userId))
                .Select(x => new GenreViewModel()
                {
                    Id = x.Id,
                    GenreName = x.GenreName,
                }).ToArray();
                    
            return result;
        }

            
    }
    
}
