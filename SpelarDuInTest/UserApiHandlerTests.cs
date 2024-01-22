using Moq;
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
    public class UserApiHandlerTests
    {
        //(UserHandler)
        //Here we're testing to see if methods in UserHandler
        //calls the methods in IUserDbHelper 
        [TestMethod]
        public void ShowAllUsersAllInfo_InUserHandler_CallsShowAllUsersAllInfo_In_IUserDbHandler()
        {
            //Arrange
            var mockService = new Mock<IUserDbHelper>();
            IUserDbHelper dbHelper = mockService.Object;

            //Act
            UserHandler.ShowAllUsersAllInfo(dbHelper);

            //Assert
            mockService.Verify(m=>m.ShowAllUsersAllInfo(), Times.Once);
        }
        [TestMethod]
        public void GetAllUsers_InUserHandler_CallsGetAllUsers_In_IUserDbHandler()
        {
            //Arrange
            var mockService = new Mock<IUserDbHelper>();
            IUserDbHelper dbHelper = mockService.Object;

            //Act
            UserHandler.GetAllUsers(dbHelper);

            //Assert
            mockService.Verify(m=>m.GetAllUsers(), Times.Once);
        }
        [TestMethod]
        public void CreateUser_InUserHandler_CallsCreateUser_In_IUserDbHandler()
        {
            //Arrange
            var mockService = new Mock<IUserDbHelper>();
            IUserDbHelper dbHelper = mockService.Object;

            UserDto user = new UserDto()
            {
                UserName = "test-user",
            };

            // Act
            UserHandler.CreateUser(dbHelper, user);

            // Assert
            mockService.Verify(x => x.CreateUser(user), Times.Once);
        }
        [TestMethod]
        public void ConnectUserToOneGenre_InUserHandler_Calls_ConnectUserToOneGenre_In_IUserDbHandler()
        {
            //Arrange
            var mockService = new Mock<IUserDbHelper>();
            IUserDbHelper dbHelper = mockService.Object;
            int userId = 1;
            int genreId = 1;

            //Act
            UserHandler.ConnectUserToOneGenre(dbHelper, userId, genreId);

            //Assert
            mockService.Verify(m=>m.ConnectUserToOneGenre(userId, genreId), Times.Once);
        }
        [TestMethod]
        public void ConnectUserToOneArtist_InUserHandler_Calls_ConnectUserToOneArtist_In_IUserDbHandler()
        {
            var mockService = new Mock<IUserDbHelper>();
            IUserDbHelper dbHelper = mockService.Object;
            int userId = 1;
            int artistId = 1;

            //Act
            UserHandler.ConnectUserToOneArtist(dbHelper, userId, artistId);

            //Assert
            mockService.Verify(m=>m.ConnectUserToOneArtist(userId, artistId), Times.Once);
        }
        [TestMethod]
        public void ConnectUserToOneTrack_InUserHandler_Calls_ConnectUserToOneTrack_In_IUserDbHandler()
        {
            //Arrange
            var mockService = new Mock<IUserDbHelper>();
            IUserDbHelper dbHelper = mockService.Object;
            int userId = 1;
            int trackId = 1;

            //Act
            UserHandler.ConnectUserToOneTrack(dbHelper, userId, trackId);

            //Assert
            mockService.Verify(m=>m.ConnectUserToOneTrack(userId, trackId), Times.Once);
        }
    }
}
