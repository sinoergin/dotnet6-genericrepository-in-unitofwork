namespace DotNet6.GenericRepositoryInUnitOfWork.Database.Entity
{
    public class Category : BaseEntity
    {
        public ICollection<Product> Products { get; set; }
    }
}
