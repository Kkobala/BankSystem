using System.Text.Json.Serialization;

namespace BankSystem.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Currency
    {
        GEL = 0,
        USD = 1,
        EUR = 2
    }
}
