using Api.Controllers.Public.Operation.Dto.request;
using AutoMapper;
using Dal.Operation.Entity;

namespace Api.Controllers.Public.Operation.Mapping;

public class UpdateOperationProfile : Profile
{
    public UpdateOperationProfile()
    {
        CreateMap<UpdateOperationModelRequest, OperationDal>()
            .ForMember(dst => dst.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dst => dst.DateTime, opt => opt.MapFrom(src => src.DateTime));
    }
}