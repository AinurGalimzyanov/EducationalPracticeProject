using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api.Controllers.Public.Message.Dto.Response;

public class GetMessage
{
    [Required] 
    [JsonProperty("Message")] 
    public string Message { get; init; }

    public GetMessage(string message)
    {
        Message = message;
    }
}