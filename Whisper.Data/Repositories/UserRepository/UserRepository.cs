using Microsoft.EntityFrameworkCore;
using Whisper.Data.Entities;
using Whisper.Data.Repositories.Base;

namespace Whisper.Data.Repositories.UserRepository;

internal sealed class UserRepository(WhisperDbContext context) : Repository<UserEntity>(context), IUserRepository
{
    public async Task<UserEntity?> GetByEmailAndPhoneNumberAsync(string email, string phoneNumber)
    {
        return await DbContext.Users
            .Where(x => x.Email == email && x.PhoneNumber == phoneNumber)
            .SingleOrDefaultAsync();
    }

    public async Task<UserEntity?> GetRelatedByIdAsync(Guid id)
    {
        return await DbContext.Users
            .Where(x => x.Id == id)
            .Include(x => x.RefreshToken)
            .SingleOrDefaultAsync();
    }

    public async Task<UserEntity?> GetByEmailAsync(string email)
    {
        return await DbContext.Users
            .Where(x => x.Email == email)
            .SingleOrDefaultAsync();
    }

    public async Task<UserEntity?> GetRelatedByEmailAsync(string email)
    {
        return await DbContext.Users
            .Where(x => x.Email == email)
            .Include(x => x.RefreshToken)
            .SingleOrDefaultAsync();
    }

    public async Task<UserEntity?> GetByPhoneNumberAsync(string phoneNumber)
    {
        return await DbContext.Users
            .Where(x => x.PhoneNumber == phoneNumber)
            .SingleOrDefaultAsync();
    }
}