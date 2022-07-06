namespace DotNet6.GenericRepositoryInUnitOfWork.GenericRepository
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, new() where TKey : struct
    {
        protected readonly DbContext DbContext;

        public Repository(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        private DbSet<TEntity> Set => DbContext.Set<TEntity>();
                
        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where = null)
        {
            if (where is null)
                return await Set.ToListAsync();
            return await Set.Where(where).ToListAsync();
        }

        public async Task<TEntity> GetAsync(TKey key)
        {
            return await Set.FindAsync(new object[] { key });
        }

        public void Insert(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            Set.Add(entity);
        }

        public void Update(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            Set.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            Set.Remove(entity);
        }
    }
}
