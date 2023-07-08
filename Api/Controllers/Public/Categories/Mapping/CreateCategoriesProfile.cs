using Api.Controllers.Public.Categories.Dto.Request;
using AutoMapper;
using Dal.Categories.Entity;

namespace Api.Controllers.Public.Categories.Mapping;

public class CreateCategoriesProfile : Profile
{
    public CreateCategoriesProfile()
    {
        CreateMap<CreateCategoriesModelRequest, CategoriesDal>()
            .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dst => dst.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dst => dst.Img, opt => opt.MapFrom(src => src.Img))
            ;
    }
}