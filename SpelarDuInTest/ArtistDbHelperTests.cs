using Microsoft.EntityFrameworkCore;
using SpelarDuInAPI.Data;
using SpelarDuInAPI.Models.DTO;
using SpelarDuInAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpelarDuInTest
{
    [TestClass]
    public class ArtistDbHelperTests
    {
        //Here we are testing to see if the changes in db goes through correctly!
        [TestMethod]
        public void AddArtist_successfullyAddNewArtistToDb()
        {
            //Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            ApplicationContext context = new ApplicationContext(options);
            ArtistDbHelper artistDbHelper = new ArtistDbHelper(context);

            //Act
            artistDbHelper.AddNewArtist(new SpelarDuInAPI.Models.DTO.ArtistDto()
            {
                ArtistName = "TestArtistName",
                Description = "TestDescription"
            });

            //Assert
            Assert.AreEqual(1, context.Artists.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddNewArtist_AddExistingArtistGivesException()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            ApplicationContext context = new ApplicationContext(options);
            ArtistDbHelper artistDbHelper = new ArtistDbHelper(context);

            artistDbHelper.AddNewArtist(new ArtistDto()
            {
                ArtistName = "TestArtistName",
                Description = "TestDescription"
            });

            // Act

            artistDbHelper.AddNewArtist(new ArtistDto()
            {
                ArtistName = "TestArtistName",
                Description = "TestDescription"
            });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddNewArtist_AddEmptyArtistGivesException()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            ApplicationContext context = new ApplicationContext(options);
            ArtistDbHelper artistDbHelper = new ArtistDbHelper(context);

            // Act
            artistDbHelper.AddNewArtist(new ArtistDto()
            {
                ArtistName = null,
                Description = null
            });
        }

    }
}
