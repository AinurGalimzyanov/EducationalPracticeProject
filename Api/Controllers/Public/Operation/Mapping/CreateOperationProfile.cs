using Api.Controllers.Public.Operation.Dto.request;
using AutoMapper;
using Dal.Operation.Entity;

namespace Api.Controllers.Public.Operation.Mapping;

public class CreateOperationProfile : Profile
{
    public CreateOperationProfile()
    {
        CreateMap<CreateOperationModelRequest, OperationDal>()
            .ForMember(dst => dst.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dst => dst.DateTime, opt => opt.MapFrom(src => src.DateTime))
            ;
    }
}