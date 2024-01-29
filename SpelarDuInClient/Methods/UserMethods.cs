using SpelarDuInAPIClient.Models;
using SpelarDuInAPIClient.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpelarDuInAPIClient.Methods
{
    public class UserMethods
    {
        public static async Task ListAllUsersAsync(HttpClient client)
        {
            
            HttpResponseMessage response = await client.GetAsync("/user"); // Anropar API endpoint som vi skapat i vår API.

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to list users {response.StatusCode}");
            }

            string content = await response.Content.ReadAsStringAsync();

            UserViewModel[] allUsers = JsonSerializer.Deserialize<UserViewModel[]>(content); // Deserialize JSON object retrieved from API

            foreach (var user in allUsers)
            {
                await Console.Out.WriteLineAsync($"{user.Id}:\t{user.UserName}");
            }

        }

        public static async Task CreateNewUserAsync(HttpClient client)
        {
            await Console.Out.WriteLineAsync("Enter your name:");

            string name = Console.ReadLine();

            if (name == null)
            {
                await Console.Out.WriteLineAsync("Name cannot be empty");
            }

            UserDto newUser = new UserDto()
            {
                UserName = name
            };

            string json = JsonSerializer.Serialize(newUser); // Seralize DTO into JSON. 

            StringContent jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/user", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                await Console.Out.WriteLineAsync($"Failed to create user (status code {response.StatusCode})");
            }

            await Console.Out.WriteLineAsync("Press enter to go back to main menu");
            Console.ReadLine();

        }

        public static async Task<UserViewModel> SelectUserAsync(HttpClient client, int userId)
        {
            HttpResponseMessage response = await client.GetAsync("/user"); // Anropar API endpoint som vi skapat i vår API.

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to list users {response.StatusCode}");
            }

            string content = await response.Content.ReadAsStringAsync();

            UserViewModel[] allUsers = JsonSerializer.Deserialize<UserViewModel[]>(content); // Deserialize JSON object retrieved from API

            UserViewModel selectedUser = allUsers
                .Where(i => i.Id == userId)
                .FirstOrDefault();

            if (selectedUser == null)
            {
                await Console.Out.WriteLineAsync("Requested user not found");
            }

            return selectedUser;

        }
    }
}
