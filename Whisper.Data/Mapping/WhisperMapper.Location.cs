using AutoMapper;
using Whisper.Data.Dtos.Location;
using Whisper.Data.Entities;
using Whisper.Data.Models;

namespace Whisper.Data.Mapping;

public partial class WhisperMapper
{
    private static void MapLocation(IMapperConfigurationExpression cfg)
    {
        cfg.CreateMap<AddLocationDto, LocationEntity>();
        cfg.CreateMap<LocationEntity, GetLocationDto>();
        cfg.CreateMap<LocationModel, LocationEntity>();
        cfg.CreateMap<LocationEntity, LocationModel>();
    }
}