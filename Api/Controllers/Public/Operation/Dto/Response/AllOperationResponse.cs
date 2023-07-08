using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api.Controllers.Public.Operation.Dto.Response;

public class AllOperationResponse
{
    [Required] 
    [JsonProperty("Operations")] 
    public List<OperationResponse> Operations { get; init; }

    public AllOperationResponse(List<OperationResponse> operations)
    {
        Operations = operations;    
    }
}