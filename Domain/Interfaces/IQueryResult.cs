namespace Domain.Interfaces
{
    public interface IQueryResult<T> where T : IEntity
    {
        int EntityCount { get; }
        int PageCount { get; }
        int PageSize { get; }
        int PageNumber { get; }
        Dictionary<string, object> Params { get; }
        IEnumerable<T> Entities { get; }
    }
}