using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using SpelarDuInAPI.Data;

namespace SpelarDuInAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("ApplicationContext");

            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));

            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            // Endpoints to be added here 

            // GET Calls

            app.MapGet("/user"); // Hämta alla personer

            app.MapGet("/user/{userId}/genre"); // Hämta alla genre kopplad till en specifik person

            app.MapGet("/user/{userId}/artist"); // Hämta alla artister kopplad till en specifik person

            app.MapGet("/user/{userId}/track"); // Hämta alla tracks kopplad till en specifik person

            // POST Calls

            app.MapPost("/user"); //skapa ny user
            app.MapPost("/genre"); //skapa ny genre
            app.MapPost("/artist"); //skapa ny artist
            app.MapPost("/track"); //skapa ny track

            app.MapPost("/user/{userId}/genre/{genreId}"); // Kopplar person till ny genre

            app.MapPost("/user/{userId}/artist/{artistId}"); //  Kopplar person till ny artist

            app.MapPost("/user/{userId}/track/{trackId}"); // Kopplar person till ny track

            app.Run();
        }
    }
}
