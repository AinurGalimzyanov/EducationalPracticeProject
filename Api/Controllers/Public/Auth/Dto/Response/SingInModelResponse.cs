using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api.Controllers.Public.Auth.Dto.Response;

public class SingInModelResponse
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
    
    [Required]
    [JsonProperty("Balance")]
    public int? Balance { get; init; }
    
    [Required] 
    [JsonProperty("Img")] 
    public string? Img { get; init; }

    public SingInModelResponse(string accessToken, string name, string email, int? balance, string? img)
    {
        AccessToken = accessToken;
        Name = name;
        Email = email;
        Balance = balance;
        Img = img;
    }
}