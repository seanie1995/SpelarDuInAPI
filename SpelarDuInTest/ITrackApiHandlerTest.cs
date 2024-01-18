using Microsoft.AspNetCore.Mvc;
using Moq;
using SpelarDuInAPI.Handlers;
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
    public class ITrackApiHandlerTest
    {
        [TestMethod]
        public void AddNewTrack_AddNewTrackToDb()
        {
            //Arrange
            var mockService = new Mock<ITrackDbHelper>();
            ITrackDbHelper dbHelper = mockService.Object;

            TrackDto track = new TrackDto()
            {
                TrackTitle = "Title",
                Artist = "ArtistTitle",
                Genre = "Genre"
            };

            // Act
            TrackHandler.AddNewTrack(dbHelper, track);

            // Assert
            mockService.Verify(x => x.AddNewTrack(track), Times.Once);
        }

        [TestMethod]
        public void GetAllTracksFromSingleUser_UserExists_ReturnsTracks()
        {
            //Arrange
            var mockService = new Mock<ITrackDbHelper>();
            int userId = 1;
            var tracks = new TrackViewModel[]
            {
                new TrackViewModel {Id = 1, TrackTitle= "Track 1"},
                new TrackViewModel {Id = 2, TrackTitle="Tracks 2"}
            };

            mockService.Setup(m => m.GetAllTracksFromSingleUser(userId)).Returns(tracks);

            // Act
            var result = TrackHandler.GetAllTracksFromSingleUser(mockService.Object, userId);

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetAllTracksFromSingleUSer_NonExistingUser_ReturnsNotFound()
        {
            //Arrange
            var mockService = new Mock<ITrackDbHelper>();
            int nonExistingUserId = 99999;

            mockService.Setup(m => m.GetAllTracksFromSingleUser(nonExistingUserId)).Throws(new Exception("User not found"));

            //Act
            var exception = Assert.ThrowsException<Exception>(()=>
            TrackHandler.GetAllTracksFromSingleUser(mockService.Object, nonExistingUserId));

            //Assert
            Assert.AreEqual("User not found", exception.Message);
        }
        [TestMethod]
        public void GetAllTracksFromSingleUSer_UserExistsButNoTracks_ReturnsEmptyList()
        {
            //Arrange
            var mockService = new Mock<ITrackDbHelper>();
            int existingUserWithNoTracks = 1;
            var emptyTracksList = new TrackViewModel[] { };

            mockService.Setup(m => m.GetAllTracksFromSingleUser(existingUserWithNoTracks)).Returns(emptyTracksList);

            //Act
            var result = TrackHandler.GetAllTracksFromSingleUser(mockService.Object, existingUserWithNoTracks);

            //Assert
            Assert.IsNotNull(result);
            var jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult);
            var tracks = jsonResult?.Value as IEnumerable<TrackViewModel>;
            Assert.IsFalse(tracks == null);
            Assert.IsFalse(tracks.Any());
        }
    }
}
