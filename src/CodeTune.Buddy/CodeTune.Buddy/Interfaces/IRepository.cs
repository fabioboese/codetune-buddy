namespace CodeTune.Buddy.Interfaces;
public interface IRepository<TEntity, TKey> where TEntity : class
{
    TEntity Add(TEntity entity);
    TEntity Update(TEntity entity);
    void Delete(TEntity entity);
    void Delete(TKey key);
    IEnumerable<TEntity>? GetAll();
    TEntity? GetById(TKey id);
    IEnumerable<TEntity>? Where(string condition);
    IEnumerable<TEntity>? Where(string condition, object parameters);
}
