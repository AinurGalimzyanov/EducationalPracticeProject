using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers.Public.Auth.Dto.Request;

public class RecoverPasswordModelRequest
{
    [Required]
    [EmailAddress]
    [JsonProperty("Email")]
    public required string Email { get; init; }
}