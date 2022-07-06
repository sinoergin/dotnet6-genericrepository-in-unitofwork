DbContextOptionsBuilder dbOptionsBuilder = new();
dbOptionsBuilder.UseInMemoryDatabase("MyDatabase");
// optionsBuilder.LogTo(Console.WriteLine);
DbContextOptions dbOptions = dbOptionsBuilder.Options;

MyDatabaseContext dbContext = new(dbOptions);
await dbContext.Database.EnsureCreatedAsync();

UnitOfWork unitOfWork = new(dbContext);
WriteLine("----------Categories----------");
foreach (var category in await unitOfWork.CategoryRepository.GetAllAsync())
{
    WriteLine($"CategoryId: {category.Id}\tCategoryName: {category.Name}");
}
WriteLine("----------Products----------");
foreach (var product in await unitOfWork.ProductRepository.GetAllAsync())
{
    WriteLine($"ProductId: {product.Id}\tProductName: {product.Name}\tPrice: {product.Price.ToString("n")}\tQuantity: {product.Quantity}");
}

ReadKey();

Category newCategory = new() { Name = "Category3" };
Product newProduct = new() { Name = "Product5", Price = 20m, Quantity = 5f, Category = newCategory };
unitOfWork.CategoryRepository.Insert(newCategory);
unitOfWork.ProductRepository.Insert(newProduct);
if (await unitOfWork.CompleteAsync())
{
    Clear();
    WriteLine("----------Categories----------");
    foreach (var category in await unitOfWork.CategoryRepository.GetAllAsync())
    {
        WriteLine($"CategoryId: {category.Id}\tCategoryName: {category.Name}");
    }
    WriteLine("----------Products----------");
    foreach (var product in await unitOfWork.ProductRepository.GetAllAsync())
    {
        WriteLine($"ProductId: {product.Id}\tProductName: {product.Name}\t" +
            $"Price: {product.Price.ToString("n")}\tQuantity: {product.Quantity}\t" +
            $"CategoryId: {product.CategoryId}");
    }
}
else
{
    WriteLine("Has been error occured on insert process.");
}
