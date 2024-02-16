using Domain;

namespace Application.Services{

    public class MovieService : IMovieService
    {
        private readonly IGenericRepository<Movie> _repo;
        
        public MovieService(IGenericRepository<Movie> repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Movie>> ListAll()
        {
            throw new NotImplementedException();
        }

        public Task<IQueryResult<Movie>> Query(int from, int to, string searchPhrase, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }
    }

};
