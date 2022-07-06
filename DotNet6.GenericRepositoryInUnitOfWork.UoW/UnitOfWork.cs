namespace DotNet6.GenericRepositoryInUnitOfWork.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly DbContext DbContext;
        public UnitOfWork(DbContext dbContext)
        {
            DbContext = dbContext; 
        }

        private IRepository<Category, int> categoryRepository;
        public IRepository<Category, int> CategoryRepository => categoryRepository ?? new Repository<Category, int>(DbContext);

        private IRepository<Product, int> productRepository;
        public IRepository<Product, int> ProductRepository => productRepository ?? new Repository<Product, int>(DbContext);

        public async Task<bool> CompleteAsync(CancellationToken cancellationToken = default)
            => await DbContext.SaveChangesAsync(cancellationToken) > 0;

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
