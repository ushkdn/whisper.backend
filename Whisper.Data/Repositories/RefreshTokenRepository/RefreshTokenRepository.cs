using Microsoft.EntityFrameworkCore;
using Whisper.Data.Entities;
using Whisper.Data.Repositories.Base;

namespace Whisper.Data.Repositories.RefreshTokenRepository;

internal sealed class RefreshTokenRepository(WhisperDbContext context) : Repository<RefreshTokenEntity>(context), IRefreshTokenRepository
{
    public async Task<RefreshTokenEntity> GetRelatedByTokenAsync(string token)
    {
        return await DbContext.RefreshTokens
            .Where(x => x.Token == token)
            .Include(x => x.User)
            .FirstOrDefaultAsync()
            ?? throw new KeyNotFoundException($"Unable to find refresh-token: {token}.");
    }
}