using Microsoft.EntityFrameworkCore;
using Whisper.Data.Entities;
using Whisper.Data.Repositories.Base;

namespace Whisper.Data.Repositories.UserRepository;

internal sealed class UserRepository(WhisperDbContext context) : Repository<UserEntity>(context), IUserRepository
{

    public async Task<UserEntity> GetByEmailOrPhoneNumberAsync(string emailOrPhoneNumber)
    {
         return await DbContext.Users.Where(x => x.Email == emailOrPhoneNumber || x.PhoneNumber == emailOrPhoneNumber)
            .FirstOrDefaultAsync() ?? throw new KeyNotFoundException("User with given email or phone number was not found");
    }
}