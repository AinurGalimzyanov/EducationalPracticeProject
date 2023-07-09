using Api.Controllers.Public.Message.Dto.Response;
using AutoMapper;
using Dal.Message.Entity;

namespace Api.Controllers.Public.Message.Mapping;

public class MessageProfile : Profile
{
    public MessageProfile()
    {
        CreateMap<MessageDal, GetMessage>()
            .ForMember(dst => dst.Message, opt => opt.MapFrom(src => src.Message))
            .ForMember(dst => dst.DateTime, opt => opt.MapFrom(src => src.DateTime));

    }
}