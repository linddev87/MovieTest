namespace Application.Interfaces
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        Task<IQueryResult<Movie>> Query(MovieQuery req);
    }
}