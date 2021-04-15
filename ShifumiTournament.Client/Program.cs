using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShifumiTournament.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new shifumiTournamentClient("http://34.90.243.63/", new HttpClient());
            var game = await client.GetGameAsync();
            Console.WriteLine(JsonConvert.SerializeObject(game));
        }
    }
}
