using Microsoft.EntityFrameworkCore;
using Whisper.Data.Entities.Base;

namespace Whisper.Data.Repositories.Base;

public class Repository<TEntity>(WhisperDbContext context) : IRepository<TEntity> where TEntity : class, IEntity
{
    protected WhisperDbContext DbContext => context;

    public void Create(TEntity entity)
    {
        if (entity is EntityBase entityBase)
        {
            entityBase.DateCreated = DateTime.Now;
        }
        context.Set<TEntity>().Add(entity);
    }

    public async Task<Lazy<int>> CreateAsync(TEntity entity)
    {
        if (entity is EntityBase entityBase)
        {
            entityBase.DateCreated = DateTime.Now;
        }
        await context.Set<TEntity>().AddAsync(entity);
        return new Lazy<int>(() => entity.Id);
    }

    public List<Lazy<int>> CreateRange(List<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            if (entity is EntityBase entityBase)
            {
                entityBase.DateCreated = DateTime.Now;
            }
        }

        context.Set<TEntity>().AddRange(entities);
        var entitiesIds = new List<Lazy<int>>();
        foreach (var entity in entities)
        {
            entitiesIds.Add(new Lazy<int>(() => entity.Id));
        }
        return entitiesIds;
    }

    public async Task<List<Lazy<int>>> CreateRangeAsync(List<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            if (entity is EntityBase entityBase)
            {
                entityBase.DateCreated = DateTime.Now;
            }
        }
        await context.Set<TEntity>().AddRangeAsync(entities);
        var entitiesId = new List<Lazy<int>>();
        foreach (var entity in entities)
        {
            entitiesId.Add(new Lazy<int>(() => entity.Id));
        }
        return entitiesId;
    }

    public void Delete(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await context.Set<TEntity>().FindAsync(id)
            ?? throw new KeyNotFoundException($"Unable to find {nameof(TEntity)} with id: {id}");
    }

    public void Update(TEntity entity)
    {
        if (entity is EntityBase entityBase)
        {
            entityBase.DateUpdated = DateTime.Now;
        }
        context.Set<TEntity>().Update(entity);
    }
}