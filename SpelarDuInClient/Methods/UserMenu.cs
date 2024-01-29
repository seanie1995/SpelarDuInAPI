﻿using Microsoft.EntityFrameworkCore;
using SpelarDuInAPIClient.Methods;
using SpelarDuInAPIClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpelarDuInClient.Methods
{
    public class UserMenu
    {
        public static async Task UsersMenuAsync(HttpClient client, int userId, UserViewModel user)
        {
            bool run = true;
            while (run)
            {
                Console.Clear();
                await Console.Out.WriteLineAsync($"Welcome {user.UserName}");
                await Console.Out.WriteLineAsync("-----------------------------");
                await Console.Out.WriteLineAsync("Choose one of the following:\n\u001b[33m[1] Add genre to user\n[2] Add artist to user\n[3] Add track to user\n[4] Go back\u001b[0m");
                string choice = Console.ReadLine();
                if (choice != "1" && choice != "2" && choice != "3" && choice != "4")
                {
                    await Console.Out.WriteLineAsync($"\u001b[31mInvalid Input![{choice}]\u001b[0m");
                    Console.ReadKey();
                    continue;
                }
                else
                {
                    switch (choice)
                    {
                        case "1":
                            await UserMethods.ConnectUserToOneGenreAsync(client, userId);
                            break;
                        case "2":
                            await UserMethods.ConnectUserToOneArtistAsync(client, userId);
                            break;
                        case "3":
                            await UserMethods.ConnectUserToOneTrackAsync(client, userId);
                            break;
                        case "4":
                            run = false;
                            break;
                    }
                }
                await Console.Out.WriteLineAsync("Press enter to go back to main menu!!");
                Console.ReadKey();
            }
        }
    }
}