using System.ComponentModel;
using Newtonsoft.Json;

namespace Api.Controllers.Public.Message.Dto.Request;

public class MessageRequest
{
    [JsonProperty("Count")]
    [DefaultValue(0)]
    public int Count { get; init; }
    
    [JsonProperty("Page")]
    [DefaultValue(0)]
    public int Page { get; init; }
}