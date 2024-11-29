using AutoMapper;
using Whisper.Data.Dtos.User;
using Whisper.Data.Entities;

namespace Whisper.Data.Mapping;

public partial class WhisperMapper
{
    private static void MapUser(IMapperConfigurationExpression cfg)
    {
        cfg.CreateMap<UserRegisterDto, UserEntity>();
        cfg.CreateMap<UserEntity, UserGetDto>();
        cfg.CreateMap<UserUpdateDto, UserEntity>();
    }
}