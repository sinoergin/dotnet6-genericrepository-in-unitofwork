namespace DotNet6.GenericRepositoryInUnitOfWork.Test
{
    [TestClass]
    public class UoWTest
    {
        public UoWTest()
        {
            DatabaseFixture dbFixture = new();
            _mockUoW = new Mock<UnitOfWork>(dbFixture.CreateContext());
        }

        private readonly Mock<UnitOfWork> _mockUoW;

        [TestMethod]
        public async Task SaveChangesForOneMoreInsert()
        {
            Category category = new() { Name = "Category3" };
            Product product = new() { Name = "Product5", Price = 5m, Quantity = 20f, Category = category };
            _mockUoW.Object.CategoryRepository.Insert(category);
            _mockUoW.Object.ProductRepository.Insert(product);
            Assert.IsTrue(await _mockUoW.Object.CompleteAsync());
        }

        [TestMethod]
        public async Task GetAllCategories()
        {
            var categories = await _mockUoW.Object.CategoryRepository.GetAllAsync();
            Assert.AreEqual(2, categories.Count);
        }

        [TestMethod]
        public async Task GetWhereCategories()
        {
            var categories = await _mockUoW.Object.CategoryRepository.GetAllAsync(p => p.Name.Contains("1"));
            Assert.AreEqual(1, categories.Count);
        }

        [TestMethod]
        public async Task GetCategory()
        {
            var category = await _mockUoW.Object.CategoryRepository.GetAsync(1);
            Assert.IsNotNull(category);
        }
    }
}