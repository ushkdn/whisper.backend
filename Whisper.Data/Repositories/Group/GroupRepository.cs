using Whisper.Data.Entities;
using Whisper.Data.Repositories.Base;

namespace Whisper.Data.Repositories.Group;

internal sealed class GroupRepository(WhisperDbContext context) : Repository<GroupEntity>(context), IGroupRepository
{
}