using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api.Controllers.Public.Categories.Dto.Response;

public class AllCategory
{
    [Required] 
    [JsonProperty("Categories")] 
    public List<CategoryResponse> Categories { get; init; }

    public AllCategory(List<CategoryResponse> categories)
    {
        Categories = categories;
    }
}