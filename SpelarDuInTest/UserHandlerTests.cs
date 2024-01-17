using Microsoft.EntityFrameworkCore;
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
        public void CreateUser_CheckIfMethodReallyCreatesUser()
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
    }
}
