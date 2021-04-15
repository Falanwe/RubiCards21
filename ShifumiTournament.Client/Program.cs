using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace ShifumiTournament.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var shifumi = new ShifumiService();
            var game = await shifumi.FindGame();
            Console.WriteLine(JsonConvert.SerializeObject(game));
        }
    }
}
