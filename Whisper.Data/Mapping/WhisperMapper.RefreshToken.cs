using AutoMapper;
using Whisper.Data.Entities;
using Whisper.Data.Models;

namespace Whisper.Data.Mapping;

public partial class WhisperMapper
{
    private static void MapRefreshToken(IMapperConfigurationExpression cfg)
    {
        cfg.CreateMap<RefreshTokenEntity, RefreshTokenModel>();
        cfg.CreateMap<RefreshTokenModel, RefreshTokenEntity>();
    }
}