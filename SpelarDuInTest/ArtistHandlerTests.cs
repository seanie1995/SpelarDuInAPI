using Microsoft.EntityFrameworkCore;
using Moq;
using SpelarDuInAPI.Data;
using SpelarDuInAPI.Handlers;
using SpelarDuInAPI.Models;
using SpelarDuInAPI.Models.DTO;
using SpelarDuInAPI.Models.ViewModels;
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
        //Here we're testing to see if methods in ArtistHandler
        //calls the methods in IArtistDbHelper

        [TestMethod]
        public void AddArtist_CallsDbHelperAddArtist()
        {
            //Arrange
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

        [TestMethod]
        public void ListUsersArtists_UserExists_ReturnsArtists()
        {
            //Arrange
            var mockService = new Mock<IArtistDbHelper>();
            IArtistDbHelper artistDbHelper = mockService.Object;
            int userId = 1;
            var artists = new ArtistViewModel[]
            {
                new ArtistViewModel
                {
                    Id = 1,
                    ArtistName = "Test-Artist1",
                    Description= "Test-Description1"
                },
                new ArtistViewModel
                { 
                    Id = 2,
                    ArtistName = "Test-Artist2",
                    Description= "Test-Description2"
                }
            };
            mockService.Setup(m => m.ListUsersArtists(userId)).Returns(artists);

            // Act
            var result = ArtistHandler.ListUsersArtists(artistDbHelper, userId);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ListUsersArtists_NonExistingUser_ReturnsNotFound()
        {
            //Arrange
            var mockService = new Mock<IArtistDbHelper>();
            IArtistDbHelper artistDbHelper = mockService.Object;
            int nonExistingUserId = 99999;

            mockService.Setup(m => m.ListUsersArtists(nonExistingUserId)).Throws(new Exception("User not found"));

            //Act
            var exception = Assert.ThrowsException<Exception>(() =>
            ArtistHandler.ListUsersArtists(artistDbHelper, nonExistingUserId));

            //Assert
            Assert.AreEqual("User not found", exception.Message);
        }
    }
}
