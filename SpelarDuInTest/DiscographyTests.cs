using DiscographyViewerAPI.Services;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Reflection.Metadata;
using DiscographyViewerAPI.Models.Dto;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Runtime.CompilerServices;

namespace SpelarDuInTest
{
    [TestClass]
    public class DiscographyTests
    {
        [TestMethod]
        public async Task GetCorrectDiscography_GetExpectedResults()
        {

            string jsonString = "{\"album\":[{\"strAlbum\":\"The Fear of Fear\",\"intYearReleased\":\"2023\"},{\"strAlbum\":\"Rotoscope\",\"intYearReleased\":\"2022\"},{\"strAlbum\":\"Eternal Blue\",\"intYearReleased\":\"2021\"}]}";

            // Arrange
            var mockHandler = new Mock<HttpClientHandler>();
            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonString)
                });
            HttpClient mockClient = new HttpClient(mockHandler.Object);
            DiscographyService service = new DiscographyService(mockClient);

            // Act

            var expectedResult = JsonSerializer.Deserialize<DiscographyDto>(jsonString);
            var result = await service.GetDiscographyAsync("shadows+fall");           
            string strResult = result.ToString();
            string strExpResult = expectedResult.ToString();
            // Assert

            Assert.AreEqual(strResult, strExpResult);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException))]
        public async Task GetCorrectDiscography_GetExceptionBadStatusCode()

        {

            string jsonString = "{\"album\":[{\"strAlbum\":\"The Fear of Fear\",\"intYearReleased\":\"2023\"},{\"strAlbum\":\"Rotoscope\",\"intYearReleased\":\"2022\"},{\"strAlbum\":\"Eternal Blue\",\"intYearReleased\":\"2021\"}]}";

            // Arrange
            var mockHandler = new Mock<HttpClientHandler>();
            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(jsonString)
                });
            HttpClient mockClient = new HttpClient(mockHandler.Object);
            DiscographyService service = new DiscographyService(mockClient);

            // Act

            var expectedResult = JsonSerializer.Deserialize<DiscographyDto>(jsonString);
            var result = await service.GetDiscographyAsync("shadows+fall");
            string strResult = result.ToString();
            string strExpResult = expectedResult.ToString();
            // Assert

            Assert.AreEqual(strResult, strExpResult);
        }
    }
}