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
using static SpelarDuInAPI.Services.IGenreDbHelper;
using Microsoft.EntityFrameworkCore;
using SpelarDuInAPI.Data;


namespace SpelarDuInTest
{
    [TestClass]
    public class GenreApiHandlerTests
    {
        [TestMethod]
        public void AddNewGenre_AddNewGenreWithApi()
        {
            var mockService = new Mock<IGenreDbHelper>();
            IGenreDbHelper dbHelper = mockService.Object;

            GenreDto genre = new GenreDto()
            {
                GenreName = "Vegetarian Slamcore",
            };

            // Act
            GenreHandler.AddNewGenre(dbHelper, genre);

            // Assert
            mockService.Verify(x => x.AddNewGenre(genre), Times.Once);
        }

       

    }
}
