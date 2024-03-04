using CodeTune.Buddy.Interfaces;
using System.Data;

namespace CodeTune.Buddy.Abstracts;
public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class
{
    protected IDbConnection conn;
    protected readonly Func<TEntity, TKey> keySelector;

    public Repository(IDbConnection conn, Func<TEntity, TKey> keySelector)
    {
        this.conn = conn;
        this.keySelector = keySelector;
    }
    public abstract TEntity Add(TEntity entity);

    public void Delete(TEntity entity)
    {
        var key = keySelector(entity);
        if (key != null)
            Delete(key);
    }

    public abstract void Delete(TKey key);

    public abstract IEnumerable<TEntity>? GetAll();

    public abstract TEntity? GetById(TKey id);

    public abstract TEntity Update(TEntity entity);

    public abstract IEnumerable<TEntity>? Where(string condition);

    public abstract IEnumerable<TEntity>? Where(string condition, object parameters);

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
