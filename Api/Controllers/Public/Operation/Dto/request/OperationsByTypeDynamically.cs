using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api.Controllers.Public.Operation.Dto.request;

public class OperationsByTypeDynamically
{
    [Required]
    [JsonProperty("DateTimeFrom")]
    public required DateTime DateTimeFrom { get; init; }
    
    [Required]
    [JsonProperty("DateTimeTo")]
    public required DateTime DateTimeTo { get; init; }
    
    [Required]
    [JsonProperty("Type")]
    public required string Type { get; init; }

    [JsonProperty("Count")]
    [DefaultValue(0)]
    public int Count { get; init; }
    
    [JsonProperty("Page")]
    [DefaultValue(0)]
    public int Page { get; init; }
}