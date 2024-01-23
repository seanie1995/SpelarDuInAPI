using Microsoft.EntityFrameworkCore;
using Moq;
using SpelarDuInAPI.Data;
using SpelarDuInAPI.Handlers;
using SpelarDuInAPI.Models;
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
    public class UserHandlerTests
    {
    //--CreateUser tests
        [TestMethod]
        public void CreateUser_Check_If_Method_Really_Creates_User()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>().UseInMemoryDatabase(databaseName: "test-db").Options;
            var context = new ApplicationContext(options);
            UserDbHelper dbHelper = new UserDbHelper(context);

            //Act
            UserHandler.CreateUser(dbHelper, new UserDto()
            {
                UserName = "Test-user",
            });
            context.SaveChanges();
            //Assert
            Assert.AreEqual(1, context.Users.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateUser_ExistingName_Throws_Exception()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>().UseInMemoryDatabase(databaseName: "test-db").Options;
            var context = new ApplicationContext(options);
            UserDbHelper dbHelper = new UserDbHelper(context);
            dbHelper.CreateUser(new UserDto { UserName = "test-username" });

            //Act & Assert
            dbHelper.CreateUser(new UserDto { UserName = "test-username" });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void CreateUser_EnterEmptyName_Throws_Exception()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>().UseInMemoryDatabase(databaseName: "test-db").Options;
            var context = new ApplicationContext(options);
            UserDbHelper dbHelper = new UserDbHelper(context);

            //Act & Assert
            dbHelper.CreateUser(new UserDto { UserName = "" });
        }




    //--Show user tests
        [TestMethod]
        public void ShowAllUsersAllInfo_Shows_Correct_Information()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>().UseInMemoryDatabase(databaseName: "test-db2").Options;
            var context = new ApplicationContext(options);
            UserDbHelper dbHelper = new UserDbHelper(context);
            var users = new List<User>
            {
                new User
                {
                    UserName = "test-user1",
                    Genres = new List<Genre>{new Genre { GenreName = "test-genre1" } },
                    Artists = new List<Artist>{new Artist { ArtistName = "test-artist1" } },
                    Tracks = new List<Track>{new Track { TrackTitle = "test-track1" } }
                },
                new User
                {
                    UserName = "test-user2",
                    Genres = new List<Genre>{new Genre { GenreName = "test-genre2" } },
                    Artists = new List<Artist>{new Artist { ArtistName = "test-artist2" } },
                    Tracks = new List<Track>{new Track { TrackTitle = "test-track2" } }
                },
            };
            foreach (var user in users)
            {
                context.Add(user);
                context.SaveChanges();
            }

            // Act
            var result = dbHelper.ShowAllUsersAllInfo();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);

            var user1 = result.Single(u => u.UserName == "test-user1");
            Assert.IsNotNull(user1);
            Assert.AreEqual(1, user1.Genres.Count);
            Assert.AreEqual("test-genre1", user1.Genres.Single().GenreName);
            Assert.AreEqual(1, user1.Artists.Count);
            Assert.AreEqual("test-artist1", user1.Artists.Single().ArtistName);
            Assert.AreEqual(1, user1.Tracks.Count);
            Assert.AreEqual("test-track1", user1.Tracks.Single().TrackTitle);

            var user2 = result.Single(u => u.UserName == "test-user2");
            Assert.IsNotNull(user2);
            Assert.AreEqual(1, user2.Genres.Count);
            Assert.AreEqual("test-genre2", user2.Genres.Single().GenreName);
            Assert.AreEqual(1, user2.Artists.Count);
            Assert.AreEqual("test-artist2", user2.Artists.Single().ArtistName);
            Assert.AreEqual(1, user2.Tracks.Count);
            Assert.AreEqual("test-track2", user2.Tracks.Single().TrackTitle);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ShowAllUsersAllInfo_Exception_if_null_users()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>().UseInMemoryDatabase(databaseName: "test-db-null").Options;
            var context = new ApplicationContext(options);
            UserDbHelper dbHelper = new UserDbHelper(context);


            // Act
            dbHelper.ShowAllUsersAllInfo();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void GetAllUsers_Exception_if_null_users()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>().UseInMemoryDatabase(databaseName: "test-db-null").Options;
            var context = new ApplicationContext(options);
            UserDbHelper dbHelper = new UserDbHelper(context);


            // Act
            dbHelper.GetAllUsers();
        }




    //--ConnectUserToGenre tests
        [TestMethod]
        public void Genre_ConnectUserToOneGenre_Correctly_Connects_User_ToGenre()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>().UseInMemoryDatabase(databaseName: "test-db-genre1").Options;
            var context = new ApplicationContext(options);
            UserDbHelper dbHelper = new UserDbHelper(context);
            var user = new User { UserName = "test-user" };
            var genre = new Genre { GenreName = "test-genre" };
            context.Users.Add(user);
            context.Genres.Add(genre);
            context.SaveChanges();

            // Act
            dbHelper.ConnectUserToOneGenre(user.Id, genre.Id);

            //Assert
            var genreUser = context.Users.Where(u=> u.Id == user.Id).Include(u=>u.Genres).SingleOrDefault();
            Assert.IsNotNull(genreUser);
            Assert.AreEqual(user.Id, genreUser.Id);
            Assert.AreEqual(1, genreUser.Genres.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void Genre_ConnectUserToOneGenre_If_wrong_UserId_Throws_Exception()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>().UseInMemoryDatabase(databaseName: "test-db-genre2").Options;
            var context = new ApplicationContext(options);
            UserDbHelper dbHelper = new UserDbHelper(context);
            var user = new User { UserName = "test-user", Id = 1 };
            var genre = new Genre { GenreName = "test-genre", Id = 1 };
            context.Users.Add(user);
            context.Genres.Add(genre);
            context.SaveChanges();

            // Act, Assert
            dbHelper.ConnectUserToOneGenre(100, genre.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Genre_ConnectUserToOneGenre_If_wrong_GenreId_Throws_Exception()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>().UseInMemoryDatabase(databaseName: "test-db-genre3").Options;
            var context = new ApplicationContext(options);
            UserDbHelper dbHelper = new UserDbHelper(context);
            var user = new User { UserName = "test-user", Id = 1 };
            var genre = new Genre { GenreName = "test-genre", Id = 1 };
            context.Users.Add(user);
            context.Genres.Add(genre);
            context.SaveChanges();

            // Act, Assert
            dbHelper.ConnectUserToOneGenre(user.Id, 100);
        }




    //--ConnectUserToOneArtist tests
        [TestMethod]
        public void Artist_ConnectUserToOneArtist_Correctly_Connects_User_ToArtist()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>().UseInMemoryDatabase(databaseName: "test-db-Artist1").Options;
            var context = new ApplicationContext(options);
            UserDbHelper dbHelper = new UserDbHelper(context);
            var user = new User { UserName = "test-user" };
            var artist = new Artist { ArtistName = "test-artist" };
            context.Users.Add(user);
            context.Artists.Add(artist);
            context.SaveChanges();

            // Act
            dbHelper.ConnectUserToOneArtist(user.Id, artist.Id);

            //Assert
            var artistUser = context.Users.Where(u => u.Id == user.Id).Include(u => u.Artists).SingleOrDefault();
            Assert.IsNotNull(artistUser);
            Assert.AreEqual(user.Id, artistUser.Id);
            Assert.AreEqual(1, artistUser.Artists.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void Artist_ConnectUserToOneArtist_If_wrong_UserId_Throws_Exception()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>().UseInMemoryDatabase(databaseName: "test-db-Artist2").Options;
            var context = new ApplicationContext(options);
            UserDbHelper dbHelper = new UserDbHelper(context);
            var user = new User { UserName = "test-user", Id = 1 };
            var artist = new Artist { ArtistName = "test-artist", Id = 1 };
            context.Users.Add(user);
            context.Artists.Add(artist);
            context.SaveChanges();

            // Act
            dbHelper.ConnectUserToOneArtist(100, artist.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Artist_ConnectUserToOneArtist_If_wrong_ArtistId_Throws_Exception()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>().UseInMemoryDatabase(databaseName: "test-db-Artist3").Options;
            var context = new ApplicationContext(options);
            UserDbHelper dbHelper = new UserDbHelper(context);
            var user = new User { UserName = "test-user", Id = 1 };
            var artist = new Artist { ArtistName = "test-artist", Id = 1 };
            context.Users.Add(user);
            context.Artists.Add(artist);
            context.SaveChanges();

            // Act
            dbHelper.ConnectUserToOneArtist(user.Id, 100);
        }




    //--ConnectUserToOneTrack tests
        [TestMethod]
        public void Track_ConnectUserToOneTrack_Correctly_Connects_User_ToArtist()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>().UseInMemoryDatabase(databaseName: "test-db-Track1").Options;
            var context = new ApplicationContext(options);
            UserDbHelper dbHelper = new UserDbHelper(context);
            var user = new User { UserName = "test-user" };
            var track = new Track { TrackTitle = "test-track" };
            context.Users.Add(user);
            context.Tracks.Add(track);
            context.SaveChanges();

            // Act
            dbHelper.ConnectUserToOneTrack(user.Id, track.Id);

            //Assert
            var trackUser = context.Users.Where(u => u.Id == user.Id).Include(u => u.Tracks).SingleOrDefault();
            Assert.IsNotNull(trackUser);
            Assert.AreEqual(user.Id, trackUser.Id);
            Assert.AreEqual(1, trackUser.Tracks.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void Track_ConnectUserToOneTrack_If_wrong_UserId_Throws_Exception()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>().UseInMemoryDatabase(databaseName: "test-db-Track2").Options;
            var context = new ApplicationContext(options);
            UserDbHelper dbHelper = new UserDbHelper(context);
            var user = new User { UserName = "test-user", Id = 1 };
            var track = new Track { TrackTitle = "test-track", Id = 1 };
            context.Users.Add(user);
            context.Tracks.Add(track);
            context.SaveChanges();

            // Act
            dbHelper.ConnectUserToOneTrack(100, track.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Track_ConnectUserToOneTrack_If_wrong_TrackId_Throws_Exception()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>().UseInMemoryDatabase(databaseName: "test-db-Track3").Options;
            var context = new ApplicationContext(options);
            UserDbHelper dbHelper = new UserDbHelper(context);
            var user = new User { UserName = "test-user", Id = 1 };
            var track = new Track { TrackTitle = "test-track", Id = 1 };
            context.Users.Add(user);
            context.Tracks.Add(track);
            context.SaveChanges();

            // Act
            dbHelper.ConnectUserToOneArtist(user.Id, 100);
        }
    }
}
