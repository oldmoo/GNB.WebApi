using System.Text.Json.Serialization;

namespace GNB.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Currency
{
     Eur,
     Usd,
     Cad,
     Aud
}