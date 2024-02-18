namespace Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _repo;
        private readonly IMemoryCache _cache;
        private readonly ILogger<MovieService> _log;

        public MovieService(IMovieRepository repo, IMemoryCache cache, ILogger<MovieService> log)
        {
            _log = log;
            _repo = repo;
            _cache = cache;
        }

        public async Task<IResult> ListAll()
        {
            try
            {
                var cacheKey = "Application.Services.MovieService.ListAll";

                if (!_cache.TryGetValue(cacheKey, out IEnumerable<Movie>? result))
                {
                    result = await _repo.ListAsync();
                    _cache.Set(cacheKey, result.OrderByDescending(m => m.CreatedDate));
                }

                return Results.Ok(result);
            }
            catch (Exception e)
            {
                _log.LogError(e, e.Message);
                return Results.Problem();
            }
        }

        public async Task<IResult> Query([AsParameters] MovieQueryRequest query)
        {
            try
            {
                var cacheKey = $"Application.Services.MovieService.Query:{query.From}_{query.To}_{query.PageSize}_{query.PageNumber}_{query.SearchPhrase}";

                if (!_cache.TryGetValue(cacheKey, out IQueryResult<Movie>? result))
                {
                    result = await _repo.Query(query);
                    _cache.Set(cacheKey, result);
                }

                return Results.Ok(result);
            }
            catch (Exception e)
            {
                _log.LogError(e, e.Message);
                return Results.Problem();
            }
        }
    }
};