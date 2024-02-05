namespace Domain.Interfaces
{
    public interface IGenericRepository<T> where T : IEntity
    {
        T? GetById(int id);
        List<T> List();
    }
}
