using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api.Controllers.Public.Operation.Dto.Response;

public class SumResponse
{
    [Required]
    [JsonProperty("Sum")]
    public decimal? Sum { get; init; }

    public SumResponse(decimal? sum)
    {
        Sum = sum;
    }
}