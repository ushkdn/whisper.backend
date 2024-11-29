using Whisper.Data.Entities;
using Whisper.Data.Repositories.Base;

namespace Whisper.Data.Repositories.LocationRepository;

internal sealed class LocationRepository(WhisperDbContext context) : Repository<LocationEntity>(context), ILocationRepository
{
}