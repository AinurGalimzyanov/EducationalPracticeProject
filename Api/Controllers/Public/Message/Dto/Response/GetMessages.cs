using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api.Controllers.Public.Message.Dto.Response;

public class GetMessages
{
    [Required] 
    [JsonProperty("Messages")] 
    public List<GetMessage> Categories { get; init; }

    public GetMessages(List<GetMessage> categories)
    {
        Categories = categories;
    }
}