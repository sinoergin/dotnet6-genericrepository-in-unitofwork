namespace DotNet6.GenericRepositoryInUnitOfWork.Database.Entity
{
    public class Product : BaseEntity
    {
        public decimal Price { get; set; }
        public float Quantity { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
