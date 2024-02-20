namespace Application.Models
{
    public class GenericQueryResult<T> : IQueryResult<T> where T : IEntity
    {
        public int EntityCount { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
        public IEnumerable<T> Entities { get; set; }

        public GenericQueryResult(int entityCount, int pageCount, int pageSize, int pageNumber, Dictionary<string, object> parameters, IEnumerable<T> entities)
        {
            EntityCount = entityCount;
            PageCount = pageCount;
            PageSize = pageSize;
            PageNumber = pageNumber;
            Parameters = parameters;
            Entities = entities;
        }
    }
};