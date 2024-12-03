using Microsoft.EntityFrameworkCore;
using Whisper.Data.Entities.Base;

namespace Whisper.Data.Repositories.Base;

public class Repository<TEntity>(WhisperDbContext context) : IRepository<TEntity> where TEntity : class, IEntity
{
    protected WhisperDbContext DbContext => context;

    public virtual void Create(TEntity entity)
    {
        if (entity is EntityBase entityBase)
        {
            entityBase.DateCreated = DateTime.Now;
        }
        context.Set<TEntity>().Add(entity);
    }

    public virtual async Task<Lazy<Guid>> CreateAsync(TEntity entity)
    {
        if (entity is EntityBase entityBase)
        {
            entityBase.DateCreated = DateTime.Now;
        }
        await context.Set<TEntity>().AddAsync(entity);
        return new Lazy<Guid>(() => entity.Id);
    }

    public virtual List<Lazy<Guid>> CreateRange(List<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            if (entity is EntityBase entityBase)
            {
                entityBase.DateCreated = DateTime.Now;
            }
        }

        context.Set<TEntity>().AddRange(entities);

        var entitiesIds = new List<Lazy<Guid>>();
        foreach (var entity in entities)
        {
            entitiesIds.Add(new Lazy<Guid>(() => entity.Id));
        }
        return entitiesIds;
    }

    public virtual async Task<List<Lazy<Guid>>> CreateRangeAsync(List<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            if (entity is EntityBase entityBase)
            {
                entityBase.DateCreated = DateTime.Now;
            }
        }
        await context.Set<TEntity>().AddRangeAsync(entities);

        var entitiesId = new List<Lazy<Guid>>();
        foreach (var entity in entities)
        {
            entitiesId.Add(new Lazy<Guid>(() => entity.Id));
        }
        return entitiesId;
    }

    public virtual void Delete(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
    }

    public virtual async Task<List<TEntity>> GetAllAsync()
    {
        return await context.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<TEntity> GetByIdAsync(Guid id)
    {
        return await context.Set<TEntity>().FindAsync(id)
            ?? throw new KeyNotFoundException($"Unable to find {nameof(TEntity)} with id: {id}");
    }

    public virtual void Update(TEntity entity)
    {
        if (entity is EntityBase entityBase)
        {
            entityBase.DateUpdated = DateTime.Now;
        }
        context.Set<TEntity>().Update(entity);
    }
}