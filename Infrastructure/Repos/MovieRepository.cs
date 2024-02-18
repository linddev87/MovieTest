namespace Infrastructure.Repos
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        private readonly ApplicationDbContext _context;

        public MovieRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IQueryResult<Movie>> Query(MovieQueryRequest req)
        {
            //Set up base query
            var query = _context.Movies.AsNoTracking();
            var paramsDict = new Dictionary<string, object>();

            //Apply filters
            if (req.From is not null)
            {
                query = query.Where(m => m.Year >= req.From);
                paramsDict.Add("From", req.From);
            }

            if (req.To is not null)
            {
                query = query.Where(m => m.Year <= req.To);
                paramsDict.Add("To", req.To);
            }

            if (!string.IsNullOrEmpty(req.SearchPhrase))
            {
                query = query.Where(m => m.TitleToLower.Contains(req.SearchPhrase, StringComparison.InvariantCultureIgnoreCase));
                paramsDict.Add("SearchPhrase", req.SearchPhrase);
            }

            var resultsCount = query.Count();

            //Apply pagination
            var entities = await
                query.OrderByDescending(m => m.Year)
                .ThenBy(m => m.Title)
                .Skip(req.PageSize * (req.PageNumber - 1))
                .Take(req.PageSize)
                .ToListAsync();

            var pageCount = (int)Math.Ceiling((decimal)resultsCount / (decimal)req.PageSize);

            //return paginated result
            return new GenericQueryResult<Movie>(
                entityCount: resultsCount,
                pageCount: pageCount,
                pageSize: req.PageSize,
                pageNumber: req.PageNumber < pageCount ? req.PageNumber : 1,
                parameters: paramsDict,
                entities: entities);
        }
    }
}