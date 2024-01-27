
using DiscographyViewerAPI.Models.Dto;
using System.Linq.Expressions;
using System.Text.Json;
using System.Net.Http;
namespace DiscographyViewerAPI.Services
{
    public interface IDiscographyService
    {
        Task<DiscographyDto> GetDiscographyAsync(string name);
    }

    public class DiscographyService : IDiscographyService
    {
        private HttpClient _client;
        public DiscographyService() : this(new HttpClient()) { }
        public DiscographyService(HttpClient client)
        {
            _client = client;
        }

        public async Task<DiscographyDto> GetDiscographyAsync(string name) // Gets a list of albums from external API

        {
            var result = await _client.GetAsync($"https://www.theaudiodb.com/api/v1/json/2/discography.php?s={name}");

            result.EnsureSuccessStatusCode();


            DiscographyDto discography = JsonSerializer.Deserialize<DiscographyDto>(await result.Content.ReadAsStringAsync());

            return discography;

        }
    }
}
