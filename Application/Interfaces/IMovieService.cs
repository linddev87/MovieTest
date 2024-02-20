namespace Application.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>?> ListAll();

        Task<IQueryResult<Movie>?> Query(MovieQuery queryRequest);
    }
}