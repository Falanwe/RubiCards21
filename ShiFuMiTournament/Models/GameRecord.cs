using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShiFuMiTournament.Models
{
    [ElasticsearchType(IdProperty = nameof(Id))]
    public class GameRecord
    {        
        public string Id { get; set; }
        public int PlayerCount { get; set; }

        public List<Play> Player1Plays { get; set; } = new List<Play>();

        public List<Play> Player2Plays { get; set; } = new List<Play>();
    }
}
