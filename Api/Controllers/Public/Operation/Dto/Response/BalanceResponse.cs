using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api.Controllers.Public.Operation.Dto.Response;

public class BalanceResponse
{
    [Required]
    [JsonProperty("Balance")]
    public decimal? Balance { get; init; }

    public BalanceResponse(decimal? balance)
    {
        Balance = balance;
    }
}