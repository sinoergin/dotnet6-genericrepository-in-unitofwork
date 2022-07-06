namespace DotNet6.GenericRepositoryInUnitOfWork.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Category, int> CategoryRepository { get; }
        IRepository<Product, int> ProductRepository { get; }
        Task<bool> CompleteAsync(CancellationToken cancellationToken = default);
    }
}
