using Microsoft.EntityFrameworkCore;
using Whisper.Data.Entities;
using Whisper.Data.Repositories.Base;

namespace Whisper.Data.Repositories.UserRepository;

public interface IUserRepository : IRepository<UserEntity>
{
    Task<UserEntity?> GetByEmailAsync(string email);

    Task<UserEntity?> GetByPhoneNumberAsync(string phoneNumber);
}