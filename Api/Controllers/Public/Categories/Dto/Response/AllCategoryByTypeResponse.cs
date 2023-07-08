using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api.Controllers.Public.Categories.Dto.Response;

public class AllCategoryByTypeResponse
{
    [Required] 
    [JsonProperty("ListIncome")] 
    public List<CategoryResponse> ListIncome { get; init; }
    
    [Required] 
    [JsonProperty("ListExpenses")] 
    public List<CategoryResponse> ListExpenses { get; init; }

    public AllCategoryByTypeResponse(List<CategoryResponse> listIncome, List<CategoryResponse> listExpenses)
    {
        ListIncome = listIncome;
        ListExpenses = listExpenses;
    }
}