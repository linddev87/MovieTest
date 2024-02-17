
namespace Api.Interfaces{
    public interface IMovieService
    {
        Task<IResult> ListAll();
        Task<IResult> Query(MovieQueryRequest queryRequest);
    }    
}

