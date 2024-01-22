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
        //Here we're testing to see if CreateUser in UserHandler
        //calls the CreateUser in IUserDbHelper 
        [TestMethod]
        public void CreateUser_InUserHandler_CallsCreateUser_In_IUserDbHandler()
        {
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
    }
}
