using Moq;
using SpelarDuInAPI.Handlers;
using SpelarDuInAPI.Models.DTO;
using SpelarDuInAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using static SpelarDuInAPI.Services.IDbHelper;


namespace SpelarDuInTest
{
    [TestClass]
    public class ApiHandlerTests
    {
        [TestMethod]
        public void AddNewGenre_AddNewGenreToDb()
        {
            var mockService = new Mock<IDbHelper>();
            IDbHelper dbHelper = mockService.Object;

            GenreDto genre = new GenreDto()
            {
                GenreName = "Vegeatarian Slamcore",
            };

            // Act
            GenreHandler.AddNewGenre(dbHelper, genre);

            // Assert
            mockService.Verify(x => x.AddNewGenre(genre), Times.Once);
        }


    }
}
