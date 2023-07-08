using Api.Controllers.Public.Auth.Dto.Request;
using AutoMapper;
using Dal.User.Entity;

namespace Api.Controllers.Public.Auth.Mapping;

public class AccountMappingProfile : Profile
{
    public AccountMappingProfile()
    {
        CreateMap<RegisterModelRequest, UserDal>()
            .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name));
    }
};