namespace DotNet6.GenericRepositoryInUnitOfWork.Database.Extension
{
    public static class ModelBuilderExtension
    {
        public static void FakeStuff(this ModelBuilder builder)
        {
            List<Category> categories = new()
            {
                new Category
                {
                    Id = 1,
                    Name = "Category1"
                },
                new Category
                {
                    Id = 2,
                    Name = "Category2"
                }
            };

            List<Product> products = new()
            {
                new Product
                {
                    Id = 1,
                    Name = "Product1",
                    Price = 10m,
                    Quantity = 5f,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Product2",
                    Price = 15m,
                    Quantity = 10f,
                    CategoryId = 2
                },
                new Product
                {
                    Id = 3,
                    Name = "Product3",
                    Price = 5m,
                    Quantity = 10f,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 4,
                    Name = "Product4",
                    Price = 3m,
                    Quantity = 15f,
                    CategoryId = 2
                }
            };

            builder.Entity<Category>().HasData(categories);
            builder.Entity<Product>().HasData(products);
        }
    }
}
