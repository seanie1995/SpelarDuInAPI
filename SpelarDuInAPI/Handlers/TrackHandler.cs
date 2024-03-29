﻿using Microsoft.EntityFrameworkCore;
using SpelarDuInAPI.Data;
using SpelarDuInAPI.Models;
using SpelarDuInAPI.Models.DTO;
using SpelarDuInAPI.Models.ViewModels;
using SpelarDuInAPI.Services;
using SpelarDuInAPI.Services.ExceptionRepository;
using System.Net;

namespace SpelarDuInAPI.Handlers
{
    public class TrackHandler
    {
        public static IResult AddNewTrack(ITrackDbHelper trackDbHelper, TrackDto trackDto)
        {
            try
            {
                trackDbHelper.AddNewTrack(trackDto);
                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return Results.StatusCode((int)(int)HttpStatusCode.InternalServerError);
            }
        }
       
        public static IResult ListAllTracks(ITrackDbHelper trackDbHelper)
        {
            try
            {
                return Results.Json(trackDbHelper.ListAllTracks());
            }
            catch (Exception ex)
            {
                return Results.Json(new { Error = "No tracks in database" });

            }
        }
        public static IResult GetAllTracksFromSingleUser(ITrackDbHelper trackDbHelper, int userId)
        {
            try
            {
                return Results.Json(trackDbHelper.GetAllTracksFromSingleUser(userId));
            }
            catch
            {
                return Results.Json(new { Error = "No tracks connected to user in database" });                    
            }

        }
    }
}
