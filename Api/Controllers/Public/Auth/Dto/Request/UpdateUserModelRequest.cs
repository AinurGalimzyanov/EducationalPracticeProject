using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Newtonsoft.Json;

namespace Api.Controllers.Public.Auth.Dto.Request;

public class UpdateUserModelRequest
{
    [JsonProperty("Name")]
    [DefaultValue(null)]
    public string? Name { get; init; }

    [EmailAddress]
    [DefaultValue(null)]
    [JsonProperty("Email")]
    public string? Email { get; init; } 

    [DefaultValue(null)]
    [DataType(DataType.Password)]
    [JsonProperty("Password")]
    public string? Password { get; init; }
    
    [DefaultValue(null)]
    [JsonProperty("Img")]
    public string? Img { get; init; }
}