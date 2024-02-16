using Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

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

        public async Task<IEnumerable<Movie>> ListAll()
        {
            try{
                var cacheKey = "Application.Services.MovieService.ListAll";

                if(!_cache.TryGetValue(cacheKey, out IEnumerable<Movie>? result)){
                    result = await _repo.ListAsync();
                }

                return result ?? new List<Movie>();
            }
            catch (Exception e){
                throw;
            }
        }

        public Task<IQueryResult<Movie>> Query(int from, int to, string searchPhrase, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }
    }

};
