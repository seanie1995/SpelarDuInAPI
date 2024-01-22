using Microsoft.EntityFrameworkCore;
using Moq;
using SpelarDuInAPI.Data;
using SpelarDuInAPI.Handlers;
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
    public class ArtistHandlerTests
    {
        [TestMethod]
        public void AddArtist_CallsDbHelperAddArtist()
        {
            //Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            var mockService = new Mock<IArtistDbHelper>();
            IArtistDbHelper artistDbHelper = mockService.Object;

            ArtistDto artistDto = new ArtistDto() //create an new artist
            {
                ArtistName = "TestArtistName",
                Description = "TestDescription"
            };

            //Act
            ArtistHandler.AddNewArtist(artistDbHelper, artistDto);

            //Assert
            mockService.Verify(h => h.AddNewArtist(artistDto), Times.Once);
        }
    }
}
