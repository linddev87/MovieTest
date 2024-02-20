namespace Application.Interfaces
{
    public interface IQueryResult<T> where T : IEntity
    {
        int EntityCount { get; set; }
        int PageCount { get; set; }
        int PageSize { get; set; }
        int PageNumber { get; set; }
        Dictionary<string, object> Parameters { get; set; }
        IEnumerable<T> Entities { get; set; }
    }
}