using Microsoft.EntityFrameworkCore;
using Moq;
using SpelarDuInAPI.Data;
using SpelarDuInAPI.Handlers;
using SpelarDuInAPI.Models;
using SpelarDuInAPI.Models.DTO;
using SpelarDuInAPI.Services;
using System.Security.Cryptography.X509Certificates;
using static SpelarDuInAPI.Services.IGenreDbHelper;

namespace SpelarDuInTest
{
    [TestClass]
    public class GenreDbHelperTests
    {
        [TestMethod]
        public void AddNewGenre_SuccessfulyAddNewGenre()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            ApplicationContext context = new ApplicationContext(options);
            GenreDbHelper dbHelper = new GenreDbHelper(context);

            // Act
            dbHelper.AddNewGenre(new GenreDto()
            {
                GenreName = "Vegetarian Slamcore"
            });

            List<Genre> genres = context.Genres.ToList();

            // Assert
            Assert.AreEqual(1, context.Genres.Count());           
            Assert.AreEqual("Vegetarian Slamcore", genres[0].GenreName);
        }



        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddNewGenre_AddExistingGenreGivesException()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("TestDb2")
                .Options;
            ApplicationContext context = new ApplicationContext(options);
            GenreDbHelper dbHelper = new GenreDbHelper(context);

            dbHelper.AddNewGenre(new GenreDto()
            {
                GenreName = "Crabcore"
            });

            // Act

            dbHelper.AddNewGenre(new GenreDto()
            {
                GenreName = "Crabcore"
            });          
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddNewGenre_AddEmptyGenreGivesException()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("TestDb2")
                .Options;
            ApplicationContext context = new ApplicationContext(options);
            GenreDbHelper dbHelper = new GenreDbHelper(context);

            // Act

            dbHelper.AddNewGenre(new GenreDto()
            {
                GenreName = null
            });
        }
    }
}