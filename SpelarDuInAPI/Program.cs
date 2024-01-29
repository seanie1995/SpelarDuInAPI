using DiscographyViewerAPI.Models.Dto;
using DiscographyViewerAPI.Models.ViewModels;
using DiscographyViewerAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using SpelarDuInAPI.Data;
using SpelarDuInAPI.Handlers;
using SpelarDuInAPI.Services;


namespace SpelarDuInAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("ApplicationContext");

            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddScoped<IGenreDbHelper, GenreDbHelper>();
            builder.Services.AddScoped<IArtistDbHelper, ArtistDbHelper>();
            builder.Services.AddScoped<IUserDbHelper, UserDbHelper>();
            builder.Services.AddScoped<ITrackDbHelper, TrackDbHelper>();
            builder.Services.AddScoped<IDiscographyService, DiscographyService>();


            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");


            // Endpoints to be added here 

            //The assignment is to create the methods designated to you, you will also create tests for your individual methods!
            //If you feel that you hawe time over, the last 3 post calls are upp for grabs for the person who want to create them 

            //Next checkup will be this monday where the time will be decided on slack later this week,

            //Keep namestandards konsistent, Artist, Genre, Track and User. 

            //Api connection vill be created after our methods are done and workload will be declared after next checkup,
            //The Api decided on is The audio DB, more information about the api is in todo.txt

            //Meny vill be created in console client where we will reuse Spelar du in bank meny functions. 

            //God luck, hawe fun!!!


            // GET Calls
            app.MapGet("/user/allinfo/{userId}", UserHandler.ShowAllUsersAllInfoOneUser); // Hämta all info om en person     Mojtaba
            app.MapGet("/user/allinfo", UserHandler.ShowAllUsersAllInfo); // Hämta alla personer     Mojtaba
            app.MapGet("/user", UserHandler.GetAllUsers); // Hämta alla personer     Mojtaba
            app.MapGet("/user/{userId}/genre", GenreHandler.ListUsersGenres); // Hämta alla genre kopplad till en specifik person     Sean
            app.MapGet("/user/{userId}/artist", ArtistHandler.ListUsersArtists); // Hämta alla artister kopplad till en specifik person     Jing
            app.MapGet("/artist", ArtistHandler.ListAllArtists); //List all the artists        Jing
            app.MapGet("/artist/{artistName}", ArtistHandler.ViewArtist); //Show a specific artist    Jing
            app.MapGet("/user/{userId}/track", TrackHandler.GetAllTracksFromSingleUser); // Hämta alla tracks kopplad till en specifik person        Jonny
            app.MapGet("/track", TrackHandler.ListAllTracks);
            

            // POST Calls           
            app.MapPost("/artist", ArtistHandler.AddNewArtist); //skapa ny artist   Jing
            app.MapPost("/track", TrackHandler.AddNewTrack); //skapa ny track     jonny
           // app.MapPost("/track/{userId}", TrackHandler.addNewTrackConnectedToSingleUser);
            app.MapPost("/genre", GenreHandler.AddNewGenre);
            app.MapPost("/user", UserHandler.CreateUser); //skapa ny user   Mojtaba
            app.MapPost("/user/{userId}/genre/{genreId}", UserHandler.ConnectUserToOneGenre); // Kopplar person till ny genre  N/A
            app.MapPost("/user/{userId}/artist/{artistId}", UserHandler.ConnectUserToOneArtist); //  Kopplar person till ny artist  N/A
            app.MapPost("/user/{userId}/track/{trackId}", UserHandler.ConnectUserToOneTrack); // Kopplar person till ny track  N/A

            app.MapGet("/{name}/albums", async (string name, IDiscographyService discographyClient) =>
            {
                DiscographyDto discography = await discographyClient.GetDiscographyAsync(name.ToLower());

                DiscographyViewModel result = new DiscographyViewModel()
                {
                    Album = discography.Album.Select(a => new AlbumViewModel()
                    {
                        Name = a.StrAlbum,
                        YearReleased = a.IntYearReleased,
                    }).ToList(),
                };

                return Results.Json(result);
            });

            app.Run();
        }
    }
}
