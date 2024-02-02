using SpelarDuInAPIClient.Methods;
using SpelarDuInAPIClient.Models;
using SpelarDuInClient.Menu;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace SpelarDuInAPIClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //enabling used symbol to show in console.
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;

            await WelcomeMenu.WelcomeSignMenu();
        }
    }
}
