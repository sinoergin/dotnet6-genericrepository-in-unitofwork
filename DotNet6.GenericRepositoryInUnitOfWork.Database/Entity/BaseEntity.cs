namespace DotNet6.GenericRepositoryInUnitOfWork.Database.Entity
{
    public class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
