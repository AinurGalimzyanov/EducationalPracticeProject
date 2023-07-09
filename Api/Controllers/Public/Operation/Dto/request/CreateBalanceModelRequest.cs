using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api.Controllers.Public.Operation.Dto.request;

public class CreateBalanceModelRequest
{
    [Required]
    [JsonProperty("NewBalance")]
    public required decimal NewBalance { get; init; }
}