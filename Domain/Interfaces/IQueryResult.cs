namespace Domain.Interfaces
{
    public interface IQueryResult<T> where T : IEntityResult
    {
        int PageCount { get; }
        int PageSize { get; }
        int PageNumber { get; }
        Dictionary<string, object> Params { get; }
        IEnumerable<T> EntityResults { get; }
    }
}
