using System.Text.Json.Serialization;

namespace NavalBattle.Models
{
	[JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RoundState
    {
        NoOnePlayed,
        P1Played,
        P2Played,
        P1Won,
        P2Won,
    }
}
