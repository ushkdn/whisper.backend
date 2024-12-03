using Whisper.Data.Entities;
using Whisper.Data.Repositories.Base;

namespace Whisper.Data.Repositories.GroupRepository;

internal sealed class GroupRepository(WhisperDbContext context) : Repository<GroupEntity>(context), IGroupRepository
{
}