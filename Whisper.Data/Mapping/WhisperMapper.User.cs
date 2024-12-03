using AutoMapper;
using Whisper.Data.Dtos.User;
using Whisper.Data.Entities;
using Whisper.Data.Models;

namespace Whisper.Data.Mapping;

public partial class WhisperMapper
{
    private static void MapUser(IMapperConfigurationExpression cfg)
    {
        cfg.CreateMap<UserRegisterDto, UserEntity>()
            .ForMember(m => m.Location,
            cfg => cfg.MapFrom(e => new LocationEntity
            {
                Country = e.Location.Country,
            }));
        cfg.CreateMap<UserEntity, UserModel>();
        cfg.CreateMap<UserModel, UserEntity>();
        cfg.CreateMap<UserEntity, UserGetDto>();
        cfg.CreateMap<UserUpdateDto, UserEntity>();
        cfg.CreateMap<UserEntity, GetUserDto>();
    }
}