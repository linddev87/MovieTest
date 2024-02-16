using Domain;
using System.Linq.Expressions;

namespace Application.Services{

    public class MovieService : IMovieService
    {
        private readonly IGenericRepository<Movie> _repo;
        private readonly IMemoryCache _cache;
        
        public MovieService(IGenericRepository<Movie> repo, IMemoryCache cache)
        {
            _repo = repo;
            _cache = cache;
        }

        public async Task<IEnumerable<MovieResult>> ListAll()
        {
            try
            {
                var cacheKey = "Application.Services.MovieService.ListAll";

                if(!_cache.TryGetValue(cacheKey, out IEnumerable<MovieResult>? result)){
                    var movieList = await _repo.ListAsync();
                    result = movieList.OrderByDescending(m => m.CreatedDate).Select(m => new MovieResult(m));
                }

                return result ?? new List<MovieResult>();
            }
            catch (Exception e){
                throw;
            }
        }

        public async Task<IQueryResult<MovieResult>> Query(MovieQueryRequest query)
        {
            try
            {
                var cacheKey = $"Application.Services.MovieService.Query:{query.From}_{query.To}_{query.PageSize}_{query.PageNumber}_{query.SearchPhrase}";

                if (!_cache.TryGetValue(cacheKey, out IEnumerable<MovieResult>? result))
                {
                    
                    var movieList = await _repo.Query(query);
                       

                    result = movieList.OrderByDescending(m => m.CreatedDate).Select(m => new MovieResult(m));
                }

                return result ?? new List<MovieResult>();
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }

};
