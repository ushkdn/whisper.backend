namespace Whisper.Data.Repositories.Base;

public interface IRepository<TEntity>
{
    Task<Lazy<Guid>> CreateAsync(TEntity entity);

    Task<List<Lazy<Guid>>> CreateRangeAsync(List<TEntity> entities);

    List<Lazy<Guid>> CreateRange(List<TEntity> entities);

    Task<List<TEntity>> GetAllAsync();

    Task<TEntity> GetByIdAsync(Guid id);

    void Create(TEntity entity);

    void Update(TEntity entity);

    void Delete(TEntity entity);
}