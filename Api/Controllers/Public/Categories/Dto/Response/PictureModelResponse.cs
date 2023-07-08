using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api.Controllers.Public.Categories.Dto.Response;

public class PictureModelResponse
{
    [Required]
    [JsonProperty("Picture")]
    public Uri Picture { get; init; }

    public PictureModelResponse(Uri picture)
    {
        Picture = picture;
    }
}