namespace Whisper.Data.Transactions;

public interface ITransactionManager
{
    int SaveChanges();

    Task<int> SaveChangesAsync();
}