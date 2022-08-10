using System.Text.Json.Serialization;
using GNB.Domain.Enums;
using Newtonsoft.Json;

namespace GNB.Application.Dtos;
[Serializable]
public class RateDto
{
     public Currency From { get; set; }
     public Currency To { get; set; }
     [JsonPropertyName("rate")]
     [JsonProperty(PropertyName ="rate")]
     public decimal Value { get; set; }
}