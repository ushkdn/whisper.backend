using Whisper.Data.Entities;
using Whisper.Data.Repositories.Base;

namespace Whisper.Data.Repositories.Location;

internal sealed class LocationRepository(WhisperDbContext context) : Repository<LocationEntity>(context), ILocationRepository
{
}