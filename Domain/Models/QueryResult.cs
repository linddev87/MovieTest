namespace Domain.Models
{
    public class QueryResult<T> : IQueryResult<T> where T : IEntity
    {
        public int PageCount { get; }
        public int PageSize { get; }
        public int PageNumber { get; }
        public Dictionary<string, object> Params { get; }
        public IEnumerable<T> Entities { get; }

        public QueryResult(int pageCount, int pageSize, int pageNumber, Dictionary<string, object> parameters, IEnumerable<T> entities)
        {
            PageCount = pageCount;
            PageSize = pageSize;
            PageNumber = pageNumber;
            Params = parameters;
            Entities = entities;
        }
    }
};