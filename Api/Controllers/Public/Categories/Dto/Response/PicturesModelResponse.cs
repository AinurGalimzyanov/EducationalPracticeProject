using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api.Controllers.Public.Categories.Dto.Response;

public class PicturesModelResponse
{
    [Required]
    [JsonProperty("Pictures")]
    public List<PictureModelResponse> Pictures { get; init; }

    public PicturesModelResponse(List<PictureModelResponse> pictures)
    {
        Pictures = pictures;
    }
}