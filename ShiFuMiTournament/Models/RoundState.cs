using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShiFuMiTournament.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RoundState
    {
        NoOnePlayed,
        P1Played,
        P2Played,
        P1Won,
        P2Won,
        Tie
    }
}
