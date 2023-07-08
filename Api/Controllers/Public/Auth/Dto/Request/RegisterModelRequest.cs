using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api.Controllers.Public.Auth.Dto.Request;

public class RegisterModelRequest
{
    [Required]
    [JsonProperty("Name")]
    public required string Name { get; init; }
    
    [Required]
    [EmailAddress]
    [JsonProperty("Email")]
    public required string Email { get; init; }
    
    [Required]
    [DataType(DataType.Password)]
    [JsonProperty("Password")]
    public required string Password { get; init; }
}