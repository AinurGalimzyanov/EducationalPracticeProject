using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api.Controllers.Public.Categories.Dto.Response;

public class OperationResponse
{
    [Required]
    [JsonProperty("Id")]
    public Guid Id { get; init; }
    
    [Required]
    [JsonProperty("Price")]
    public int? Price { get; init; }
    
    [Required]
    [JsonProperty("DateTime")]
    public DateTime? DateTime { get; init; }

    public OperationResponse(Guid id, int? price, DateTime? dateTime)
    {
        Id = id;
        Price = price;
        DateTime = dateTime;
    }
}