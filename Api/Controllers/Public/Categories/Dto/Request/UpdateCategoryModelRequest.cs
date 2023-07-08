using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api.Controllers.Public.Categories.Dto.Request
{
    public class UpdateCategoryModelRequest
    {
        [Required]
        [JsonProperty("CategoryId")]
        public required Guid CategoryId { get; init; }
    
        [Required]
        [JsonProperty("Name")]
        public required string Name { get; init; }
        
        [JsonProperty("Img")]
        [DefaultValue(null)]
        public  string? Img { get; init; }
    
        [Required]
        [JsonProperty("Type")]
        public required string Type { get; init; }
    }
}