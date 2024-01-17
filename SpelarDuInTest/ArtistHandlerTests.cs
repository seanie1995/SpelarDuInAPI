using Microsoft.EntityFrameworkCore;
using SpelarDuInAPI.Data;
using SpelarDuInAPI.Handlers;
using SpelarDuInAPI.Models;
using SpelarDuInAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpelarDuInTest
{
    [TestClass]
    public class ArtistHandlerTests
    {
        [TestMethod]
        public void AddNewArtistToDatabase()
        {
            //Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            ApplicationContext context = new ApplicationContext(options);
            ArtistDto newArtist = new ArtistDto()
            {
                ArtistName = "Test_Name",
                Description = "Test_Description"
            };

            //Act
            ArtistHandler.AddNewArtist(context, newArtist);

            //Assert
            Assert.AreEqual(7, context.Artists.Count());
            Artist actual = context.Artists.Single();
            Assert.AreEqual(newArtist.ArtistName, actual.ArtistName);
            Assert.AreEqual(newArtist.Description, actual.Description);
        }
    }
}
