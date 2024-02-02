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

            //Connecting to DB
            string connectionString = builder.Configuration.GetConnectionString("ApplicationContext");

            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));

            //Dependency injections, to be able to inversion of control. 
            builder.Services.AddScoped<IGenreDbHelper, GenreDbHelper>();
            builder.Services.AddScoped<IArtistDbHelper, ArtistDbHelper>();
            builder.Services.AddScoped<IUserDbHelper, UserDbHelper>();
            builder.Services.AddScoped<ITrackDbHelper, TrackDbHelper>();
            builder.Services.AddScoped<IDiscographyService, DiscographyService>();


            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            // GET Calls
            // app.MapGet("/user/allinfo", UserHandler.ShowAllUsersAllInfo); //-working but not in use
            app.MapGet("/user/allinfo/{userId}", UserHandler.ShowOneUserAllInfo);
            app.MapGet("/user", UserHandler.GetAllUsers);
            app.MapGet("/user/{userId}/genre", GenreHandler.ListUsersGenres);
            app.MapGet("/user/{userId}/artist", ArtistHandler.ListUsersArtists);
            app.MapGet("/artist", ArtistHandler.ListAllArtists);
            app.MapGet("/artist/{artistId}", ArtistHandler.ViewArtist);
            app.MapGet("/user/{userId}/track", TrackHandler.GetAllTracksFromSingleUser);
            app.MapGet("/genre", GenreHandler.ListAllGenres);
            app.MapGet("/track", TrackHandler.ListAllTracks);
            

            // POST Calls           
            app.MapPost("/artist", ArtistHandler.AddNewArtist);
            app.MapPost("/track", TrackHandler.AddNewTrack);
            app.MapPost("/genre", GenreHandler.AddNewGenre);
            app.MapPost("/user", UserHandler.CreateUser);
            app.MapPost("/user/{userId}/genre/{genreId}", UserHandler.ConnectUserToOneGenre);
            app.MapPost("/user/{userId}/artist/{artistId}", UserHandler.ConnectUserToOneArtist);
            app.MapPost("/user/{userId}/track/{trackId}", UserHandler.ConnectUserToOneTrack);

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
