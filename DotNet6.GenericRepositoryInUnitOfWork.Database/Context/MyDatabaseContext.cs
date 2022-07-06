namespace DotNet6.GenericRepositoryInUnitOfWork.Database.Context
{
    public class MyDatabaseContext : DbContext
    {
        public MyDatabaseContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasKey(k => k.Id);
            modelBuilder.Entity<Product>().HasKey(k => k.Id);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(fk => fk.CategoryId);

            modelBuilder.FakeStuff();
        }
    }
}
