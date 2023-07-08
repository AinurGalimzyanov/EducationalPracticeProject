using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Dal.Operation.Entity;
using Newtonsoft.Json;

namespace Api.Controllers.Public.Categories.Dto.Response;

public class CategoryResponse
{
    [Required] 
    [JsonProperty("Name")] 
    public string Name { get; init; }
    
    [Required] 
    [JsonProperty("Id")] 
    public Guid Id { get; init; }
     
    [JsonProperty("Type")] 
    public string Type { get; init; }

    [JsonProperty("Img")] 
    [DefaultValue(null)]
    public string? Img { get; init; }
    
    [Required]
    [JsonProperty("Sum")] 
    public int? Sum { get; init; } = 0;
    
    [JsonProperty("ListOperation")]
    public List<OperationResponse> ListOperation { get; set; }

    public CategoryResponse(string name, Guid id, string type, int? sum, string? img = null)
    {
        Name = name;
        Id = id;
        Type = type;
        Sum = sum;
        Img = img;
    }

    public CategoryResponse(string name, Guid id, int? sum, List<OperationResponse> operationDals, string? img = null)
    {
        Name = name;
        Id = id;
        ListOperation = operationDals;
        Sum = sum;
        Img = img;
    }
}