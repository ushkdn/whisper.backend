﻿using Whisper.Data.Entities;
using Whisper.Data.Repositories.Base;

namespace Whisper.Data.Repositories.UserRepository;

public interface IUserRepository : IRepository<UserEntity>
{
    Task<UserEntity?> GetByEmailAndPhoneNumberAsync(string email, string phoneNumber);

    Task<UserEntity?> GetByEmailAsync(string email);
    Task<UserEntity?> GetByUsernameAsync(string username);

    Task<UserEntity?> GetRelatedByIdAsync(Guid id);

    Task<UserEntity?> GetRelatedByEmailAsync(string email);

    Task<UserEntity?> GetByPhoneNumberAsync(string phoneNumber);
}