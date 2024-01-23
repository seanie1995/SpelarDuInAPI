using Microsoft.EntityFrameworkCore;
using SpelarDuInAPI.Data;
using SpelarDuInAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpelarDuInTest
{
    [TestClass]
    public class ArtistDbHelperTests
    {
        [TestMethod]
        public void AddArtist_AddNewArtistToDb()
        {
            //Arrange
            DbContextOptions<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            ApplicationContext context = new ApplicationContext(options);
            ArtistDbHelper artistDbHelper = new ArtistDbHelper(context);

            //Act
            artistDbHelper.AddNewArtist(new SpelarDuInAPI.Models.DTO.ArtistDto()
            {
                ArtistName = "TestArtistName",
                Description = "TestDescription"
            });

            //Assert
            Assert.AreEqual(1, context.Artists.Count());
        }
    }
}
