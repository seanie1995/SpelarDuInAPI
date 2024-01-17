using Microsoft.EntityFrameworkCore;
using SpelarDuInAPI.Data;
using SpelarDuInAPI.Models;
using SpelarDuInAPI.Models.DTO;
using SpelarDuInAPI.Models.ViewModels;
using SpelarDuInAPI.Services;
using System.Net;

namespace SpelarDuInAPI.Handlers
{
    public class TrackHandler
    {
        
        public static IResult AddNewTrack(ITrackDbHelper trackDbHelper, TrackDto trackDto)
        {
            trackDbHelper.AddNewTrack(trackDto);
            return Results.StatusCode((int)HttpStatusCode.Created);
        }
        public static IResult GetAllTracksFromSingleUser(ITrackDbHelper trackDbHelper, int userId)
        {   
            return Results.Json(trackDbHelper.GetAllTracksFromSingleUser(userId));
        }
    }
}
