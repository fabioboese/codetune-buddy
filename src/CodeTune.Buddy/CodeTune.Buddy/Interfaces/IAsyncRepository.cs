namespace CodeTune.Buddy.Interfaces;
public interface IAsyncRepository<TEntity, TKey> where TEntity : IEntityModel
{
    TKey GetKey(TEntity entity);
    void SetKey(TKey key, TEntity entity);
    Task<TEntity> AddAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task DeleteAsync(TKey key);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<IEnumerable<TEntity>?> GetAllAsync();
    Task<TEntity?> GetByIdAsync(TKey id);
    Task<long> CountAllAsync();
    Task<long> CountAsync(string condition);
    Task<long> CountAsync(string condition, object parameters);
    Task<IEnumerable<TEntity>?> WhereAsync(string condition);
    Task<IEnumerable<TEntity>?> WhereAsync(string condition, object parameters);
}
