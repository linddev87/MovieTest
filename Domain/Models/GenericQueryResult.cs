namespace Domain.Models
{
    public class GenericQueryResult<T> : IQueryResult<T> where T : IEntity
    {
        public int EntityCount { get; }
        public int PageCount { get; }
        public int PageSize { get; }
        public int PageNumber { get; }
        public Dictionary<string, object> Params { get; }
        public IEnumerable<T> Entities { get; }

        public GenericQueryResult(int entityCount, int pageCount, int pageSize, int pageNumber, Dictionary<string, object> parameters, IEnumerable<T> entities)
        {
            EntityCount = entityCount;
            PageCount = pageCount;
            PageSize = pageSize;
            PageNumber = pageNumber;
            Params = parameters;
            Entities = entities;
        }
    }
};