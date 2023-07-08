using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api.Controllers.Public.Auth.Dto.Response;

public class RegistModelResponse
{
    [Required] 
    [JsonProperty("AccessToken")] 
    public string AccessToken { get; init; }

    [Required] 
    [JsonProperty("Name")] 
    public string Name { get; init; }
    
    [Required] 
    [EmailAddress]
    [JsonProperty("Email")] 
    public string Email { get; init; }

    public RegistModelResponse(string accessToken, string name, string email)
    {
        AccessToken = accessToken;
        Name = name;
        Email = email;
    }
}