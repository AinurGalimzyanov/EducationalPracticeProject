using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api.Controllers.Public.Operation.Dto.request;

public class CreateOperationModelRequest
{
    [Required]
    [JsonProperty("Price")]
    public required int Price { get; init; }
    
    [Required]
    [JsonProperty("DateTime")]
    public required DateTime DateTime { get; init; }
    
    [Required]
    [JsonProperty("CategoryId")]
    public required Guid CategoryId { get; init; }
}