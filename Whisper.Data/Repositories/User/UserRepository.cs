using Whisper.Data.Entities;
using Whisper.Data.Repositories.Base;

namespace Whisper.Data.Repositories.User;

internal sealed class UserRepository(WhisperDbContext context) : Repository<UserEntity>(context), IUserRepository
{
}