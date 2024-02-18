namespace Domain.Interfaces
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        Task<IQueryResult<Movie>> Query(MovieQueryRequest req);
    }
}