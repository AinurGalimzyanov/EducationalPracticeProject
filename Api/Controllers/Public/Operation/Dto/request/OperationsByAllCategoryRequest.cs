using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api.Controllers.Public.Operation.Dto.request;

public class OperationsByAllCategoryRequest
{
    [Required]
    [JsonProperty("FromDateTime")]
    public required DateTime FromDateTime { get; init; }
    
    [Required]
    [JsonProperty("ToDateTime")]
    public required DateTime ToDateTime { get; init; }
}