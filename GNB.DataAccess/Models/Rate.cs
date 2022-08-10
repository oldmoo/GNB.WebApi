using GNB.Domain.Enums;
using Newtonsoft.Json;

namespace GNB.Infrastructure.Models;

public class Rate
{
     public Currency From { get; init; }
     public Currency To { get; init; }
     [JsonProperty(PropertyName ="rate")]
     public decimal Value { get; set; }
}