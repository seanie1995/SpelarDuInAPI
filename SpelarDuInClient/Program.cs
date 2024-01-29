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
            await MenuAction.MainMenu();
        }
    }
}
