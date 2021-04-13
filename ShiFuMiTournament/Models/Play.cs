using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShiFuMiTournament.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Play
    {
        Rock,
        Paper,
        Scissors
    }
}
