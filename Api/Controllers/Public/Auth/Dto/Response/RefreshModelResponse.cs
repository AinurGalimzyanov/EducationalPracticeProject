using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api.Controllers.Public.Auth.Dto.Response;

public class RefreshModelResponse
{
    [Required] 
    [JsonProperty("AccessToken")] 
    public string AccessToken { get; init; }

    public RefreshModelResponse(string accessToken)
    {
        AccessToken = accessToken;
    }
}