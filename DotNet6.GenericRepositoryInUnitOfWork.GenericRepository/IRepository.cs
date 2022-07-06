namespace DotNet6.GenericRepositoryInUnitOfWork.GenericRepository
{
    public interface IRepository<TEntity, TKey> where TEntity : class, new() where TKey : struct
    {
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where = null);
        Task<TEntity> GetAsync(TKey key);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
