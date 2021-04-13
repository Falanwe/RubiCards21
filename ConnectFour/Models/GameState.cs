using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace ConnectFour.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum GameState
    {
        NoOnePlaying,
        PlayerOnePlaying,
        PlayerTwoPlaying,
        PlayerOneWon,
        PlayerTwoWon,
        Tie
    }
}
