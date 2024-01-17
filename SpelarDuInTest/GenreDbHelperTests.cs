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
        public void AddNewGenre_AddGenreViaHandler()
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

            // Assert
            Assert.AreEqual(1, context.Genres.Count());
        }
    }
}