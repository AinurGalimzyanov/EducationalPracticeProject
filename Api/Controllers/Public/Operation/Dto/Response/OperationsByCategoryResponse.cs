using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api.Controllers.Public.Operation.Dto.Response;

public class OperationsByCategoryResponse
{
    [Required] 
    [JsonProperty("Operations")] 
    public List<OperationResponse> Operations { get; init; }

    public OperationsByCategoryResponse(List<OperationResponse> operations)
    {
        Operations = operations;
    }
}