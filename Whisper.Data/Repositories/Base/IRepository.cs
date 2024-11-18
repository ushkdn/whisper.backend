namespace Whisper.Data.Repositories.Base;

public interface IRepository<TEntity>
{
    Task<Lazy<int>> CreateAsync(TEntity entity);

    Task<List<Lazy<int>>> CreateRangeAsync(List<TEntity> entities);

    List<Lazy<int>> CreateRange(List<TEntity> entities);

    Task<List<TEntity>> GetAllAsync();

    Task<TEntity> GetByIdAsync(int id);

    void Create(TEntity entity);

    void Update(TEntity entity);

    void Delete(TEntity entity);
}