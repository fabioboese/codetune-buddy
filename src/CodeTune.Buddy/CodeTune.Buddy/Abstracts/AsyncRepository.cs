using AutoMapper;
using CodeTune.Buddy.Interfaces;
using System.Data;

namespace CodeTune.Buddy.Abstracts;
public abstract class AsyncRepository<TEntity, TKey> : IAsyncRepository<TEntity, TKey>
where TEntity : class, IEntityModel
{
    protected IDbConnection conn;
    private readonly Func<TEntity, TKey> keyGetter;
    private readonly IMapper mapper;

    public AsyncRepository(IDbConnection conn, Func<TEntity, TKey> keyGetter)
    {
        this.conn = conn;
        this.keyGetter = keyGetter;
        MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<TKey, TEntity>();
        });
        mapper = mapperConfiguration.CreateMapper();
    }

    public virtual TKey GetKey(TEntity entity) => keyGetter(entity);

    public virtual void SetKey(TKey key, TEntity entity) => mapper.Map(key, entity);

    public abstract Task<TEntity> AddAsync(TEntity entity);

    public virtual async Task DeleteAsync(TEntity entity)
    {
        var key = GetKey(entity);
        if (key != null)
            await DeleteAsync(key);
    }

    public abstract Task DeleteAsync(TKey key);

    public abstract Task<TEntity> UpdateAsync(TEntity entity);

    public abstract Task<IEnumerable<TEntity>?> GetAllAsync();

    public abstract Task<TEntity?> GetByIdAsync(TKey id);

    public abstract Task<long> CountAllAsync();

    public abstract Task<long> CountAsync(string condition);

    public abstract Task<long> CountAsync(string condition, object parameters);

    public abstract Task<IEnumerable<TEntity>?> WhereAsync(string condition);

    public abstract Task<IEnumerable<TEntity>?> WhereAsync(string condition, object parameters);

    protected virtual bool OnBeforeCreate(TEntity entity)
    {
        return true;
    }

    protected virtual bool OnBeforeDelete(TEntity entity)
    {
        return true;
    }

    protected virtual bool OnBeforeUpdate(TEntity entity)
    {
        return true;
    }

    protected virtual void OnAfterCreate(TEntity entity)
    {
    }

    protected virtual void OnAfterDelete(TEntity entity)
    {
    }

    protected virtual void OnAfterUpdate(TEntity entity)
    {
    }
}
