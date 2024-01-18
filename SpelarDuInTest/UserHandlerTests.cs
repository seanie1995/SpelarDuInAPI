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

            var user1 = result.First(u => u.UserName == "test-user1");
            Assert.IsNotNull(user1);
            Assert.AreEqual(1, user1.Genres.Count);
            Assert.AreEqual("test-genre1", user1.Genres.First().GenreName);
            Assert.AreEqual(1, user1.Artists.Count);
            Assert.AreEqual("test-artist1", user1.Artists.First().ArtistName);
            Assert.AreEqual(1, user1.Tracks.Count);
            Assert.AreEqual("test-track1", user1.Tracks.First().TrackTitle);

            var user2 = result.First(u => u.UserName == "test-user2");
            Assert.IsNotNull(user2);
            Assert.AreEqual(1, user2.Genres.Count);
            Assert.AreEqual("test-genre2", user2.Genres.First().GenreName);
            Assert.AreEqual(1, user2.Artists.Count);
            Assert.AreEqual("test-artist2", user2.Artists.First().ArtistName);
            Assert.AreEqual(1, user2.Tracks.Count);
            Assert.AreEqual("test-track2", user2.Tracks.First().TrackTitle);
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
    }
}
