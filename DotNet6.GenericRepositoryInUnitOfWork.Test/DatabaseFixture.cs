namespace DotNet6.GenericRepositoryInUnitOfWork.Test
{
    public class DatabaseFixture
    {
        private readonly object _lock = new object();
        private readonly bool _databaseInit;
        public DatabaseFixture()
        {
            lock (_lock)
            {
                if (!_databaseInit)
                {
                    using var context = CreateContext();
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                }
            }
        }

        public MyDatabaseContext CreateContext()
            => new MyDatabaseContext(
                new DbContextOptionsBuilder<MyDatabaseContext>()
                .UseInMemoryDatabase("MyDatabase")
                .Options);
    }
}
