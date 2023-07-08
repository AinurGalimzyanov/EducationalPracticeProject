using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api.Controllers.Public.Operation.Dto.request;

public class AllOperationModelRequest
{
    [Required]
    [JsonProperty("DateTime")]
    public required DateTime DateTime { get; init; }

    [JsonProperty("Count")]
    [DefaultValue(0)]
    public int Count { get; init; }
    
    [JsonProperty("Page")]
    [DefaultValue(0)]
    public int Page { get; init; }
}