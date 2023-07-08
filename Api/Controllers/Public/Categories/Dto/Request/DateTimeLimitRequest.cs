using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api.Controllers.Public.Categories.Dto.Request;

public class DateTimeLimitRequest
{
    [Required]
    [JsonProperty("FromDateTime")]
    public required DateTime FromDateTime { get; init; }
    
    [Required]
    [JsonProperty("ToDateTime")]
    public required DateTime ToDateTime { get; init; }
    
    [Required]
    [JsonProperty("Type")]
    public required string Type { get; init; }
}