using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SpelarDuInAPI.Data;
using SpelarDuInAPI.Handlers;
using SpelarDuInAPI.Models;
using SpelarDuInAPI.Models.DTO;
using System.ComponentModel;
using System;
using System.Net;
using System.Net.NetworkInformation;
using Newtonsoft.Json;

namespace SpelarDuInTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddNewGenre_AddGenreViaHandler()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            ApplicationContext context = new ApplicationContext(options);

            // Act
            GenreHandler.AddNewGenre(context, new GenreDto()
            {
                GenreName = "Vegetarian Slamcore"
            });

            // Assert
            Assert.AreEqual(1, context.Genres.Count());

            var addedGenre = context.Genres.Where(x => x.GenreName == "Vegetarian Slamcore").First(); ;

            Assert.AreEqual("Vegetarian Slamcore", addedGenre.GenreName);
        }

        [TestMethod]
        public void AddNewGenre_AddGenreViaHandlerCheckName()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            ApplicationContext context = new ApplicationContext(options);

            // Act
            GenreHandler.AddNewGenre(context, new GenreDto()
            {
                GenreName = "Vegetarian Slamcore"
            });

            var addedGenre = context.Genres.Where(x => x.GenreName == "Vegetarian Slamcore").First(); ;
            // Assert

            Assert.AreEqual("Vegetarian Slamcore", addedGenre.GenreName);
        }
        [TestMethod]
        public void AddNewTrack_AddTrackViaHandler()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            ApplicationContext context = new ApplicationContext(options);

            TrackDto track = new TrackDto()
            {
                TrackTitle = "Title",
                Artist = "ArtistTitle",
                Genre = "Genre"
            };
            // Act
            TrackHandler.AddNewTrack(context, track);

            //Assert
            Assert.AreEqual(1, context.Tracks.Count());
            Track actual = context.Tracks.Single();
            Assert.AreEqual(track.TrackTitle, actual.TrackTitle);
            Assert.AreEqual(track.Artist, actual.Artist.ArtistName);
            Assert.AreEqual(track.Genre, actual.Genre.GenreName);
        }
      /*  [TestMethod]
        public void GetAllTracksFromSingleUser_WithNonExistingUserId_ReturnsNotFound()
        {
            // Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            ApplicationContext context = new ApplicationContext(options);
            var httpContext = new DefaultHttpContext();

            // Act
            TrackHandler.GetAllTracksFromSingleUser(context, 3);

            //Assert

            Assert.AreEqual(StatusCodes.Status404NotFound, httpContext.Response.StatusCode);
        }*/
    }
}