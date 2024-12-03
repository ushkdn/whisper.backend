namespace Whisper.Data.Transactions;

public sealed class TransactionManager(WhisperDbContext context)
    : ITransactionManager, IDisposable, IAsyncDisposable
{
    internal WhisperDbContext DbContext => context;

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }

    public int SaveChanges()
    {
        return context.SaveChanges();
    }

    public void Dispose()
    {
        DbContext.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await DbContext.DisposeAsync();
    }
}