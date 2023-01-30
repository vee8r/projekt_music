using System.Text.Json.Serialization;

namespace projekt_programowanie.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Type
    {
        Rap = 1,

        Rock = 2,

        Pop = 3,

        Hip_hop = 4,

        Classical = 5,

        Electronic = 6,
    }

}
