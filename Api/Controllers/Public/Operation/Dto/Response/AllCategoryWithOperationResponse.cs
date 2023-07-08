using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api.Controllers.Public.Operation.Dto.Response;

public class AllCategoryWithOperationResponse
{
    [Required] 
    [JsonProperty("Category")] 
    public List<AllOperationResponse> Operations { get; init; }

    public AllCategoryWithOperationResponse(List<AllOperationResponse> operations)
    {
        Operations = operations;
    }
}