using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api.Controllers.Public.Operation.Dto.request;

public class SumByTypeModelRequest
{
    [Required]
    [JsonProperty("DateTime")]
    public required DateTime DateTime { get; init; }
    
    [Required]
    [JsonProperty("Type")]
    public required string Type { get; init; }
}