using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api.Controllers.Public.Operation.Dto.request;

public class OperationsByCategoryModelRequest
{
    [Required]
    [JsonProperty("CategoryId")]
    public required Guid CategoryId { get; init; }
    
    [Required]
    [JsonProperty("DateTime")]
    public required DateTime DateTime { get; init; }
    
    [Required]
    [JsonProperty("Quantity")]
    [DefaultValue(0)]
    public required int Quantity { get; init; }
}