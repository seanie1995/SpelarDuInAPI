using Microsoft.EntityFrameworkCore;
using SpelarDuInAPI.Data;
using SpelarDuInAPI.Handlers;
using SpelarDuInAPI.Models.DTO;
using SpelarDuInAPI.Models;
using SpelarDuInAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace SpelarDuInTest
{
    [TestClass]
    public class TrackDbHelperTest
    {
        [TestMethod]
        public void AddNewTrack_SuccessfulyAddNewGenre()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                 .UseInMemoryDatabase("TestDb")
                 .Options;
            ApplicationContext context = new ApplicationContext(options);
            TrackDbHelper dbHelper = new TrackDbHelper(context);

            TrackDto track = new TrackDto()
            {
                TrackTitle = "Title",
                Artist = "ArtistTitle",
                Genre = "Genre"
            };
            // Act
            TrackHandler.AddNewTrack(dbHelper, track);

            //Assert
            Assert.AreEqual(1, context.Tracks.Count());
            Track actual = context.Tracks.Single();
            Assert.AreEqual(track.TrackTitle, actual.TrackTitle);
            Assert.AreEqual(track.Artist, actual.Artist.ArtistName);
            Assert.AreEqual(track.Genre, actual.Genre.GenreName);
        }
       
    }
}
