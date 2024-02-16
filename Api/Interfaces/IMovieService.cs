namespace Api.Interfaces{
    public interface IMovieService
    {
        Task<IEnumerable<MovieResult>> ListAll();
        Task<IQueryResult<MovieResult>> Query(MovieQueryRequest queryRequest);
    }    
}

