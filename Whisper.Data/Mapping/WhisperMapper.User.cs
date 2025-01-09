using AutoMapper;
using Whisper.Data.Dtos.User;
using Whisper.Data.Entities;
using Whisper.Data.Models;

namespace Whisper.Data.Mapping;

public partial class WhisperMapper
{
    private static void MapUser(IMapperConfigurationExpression cfg)
    {
        cfg.CreateMap<UserRegisterDto, UserModel>()
            .ForMember(m => m.Location,
            cfg => cfg.MapFrom(e => new LocationModel
            {
                Country = e.Location.Country,
            }));

        cfg.CreateMap<UserUpdateDto, UserEntity>();
        cfg.CreateMap<UserEntity, UserModel>();
        cfg.CreateMap<UserModel, UserEntity>();
        cfg.CreateMap<UserModel, UserGetDto>();
        cfg.CreateMap<UserModel, GetUserDto>();
    }
}