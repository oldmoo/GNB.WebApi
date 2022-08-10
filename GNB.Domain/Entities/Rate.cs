using GNB.Domain.Enums;
using Newtonsoft.Json;

namespace GNB.Domain.Entities;

public class Rate : BaseEntity
{
     public Currency From { get; set; }
     public Currency To { get; set; }
     [JsonProperty(PropertyName ="rate")]
     public decimal Value { get; set; }
}