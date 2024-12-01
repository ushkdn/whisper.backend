using AutoMapper;
using Whisper.Data.Dtos.Location;
using Whisper.Data.Entities;

namespace Whisper.Data.Mapping;

public partial class WhisperMapper
{
    private static void MapLocation(IMapperConfigurationExpression cfg)
    {
        cfg.CreateMap<AddLocationDto, LocationEntity>();
        cfg.CreateMap<LocationEntity, GetLocationDto>();
    }
}