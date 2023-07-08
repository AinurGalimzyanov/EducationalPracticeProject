using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api.Controllers.Public.Auth.Dto.Response;

public class UpdateUserModelResponse
{
    [Required] 
    [JsonProperty("AccessToken")] 
    public string AccessToken { get; init; }

    public UpdateUserModelResponse(string accessToken)
    {
        AccessToken = accessToken;
    }
}