namespace DotNet6.GenericRepositoryInUnitOfWork.Database.Entity
{
    public interface IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
