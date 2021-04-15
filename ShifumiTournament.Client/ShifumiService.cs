using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShifumiTournament.Client
{
    public class ShifumiService
    {
        public async Task<Game> FindGame()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("http://34.90.243.63/ShiFuMi/game");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Game>(json);
        }
    }
}
