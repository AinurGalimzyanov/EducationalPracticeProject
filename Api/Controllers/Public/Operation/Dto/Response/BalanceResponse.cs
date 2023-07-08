using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api.Controllers.Public.Operation.Dto.Response;

public class BalanceResponse
{
    [Required]
    [JsonProperty("Balance")]
    public int? Balance { get; init; }

    public BalanceResponse(int? balance)
    {
        Balance = balance;
    }
}