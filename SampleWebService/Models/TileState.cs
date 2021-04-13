using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace SampleWebService.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TileState
    {
        Empty, 
        Red,
        Yellow
    }
}
