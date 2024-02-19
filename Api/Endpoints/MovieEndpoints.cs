namespace Api.Endpoints
{
    /// <summary>
    /// Thin endpoint class which converts results from the application layer to HttpResponses
    /// </summary>
    public class MovieEndpoints
    {
        private readonly ILogger<MovieEndpoints> _log;
        private readonly IMovieService _movieService;

        public MovieEndpoints(ILogger<MovieEndpoints> log, IMovieService movieService, IMemoryCache cache)
        {
            _log = log;
            _movieService = movieService;
        }

        internal async Task<IResult> ListAll()
        {
            try
            {
                var result = await _movieService.ListAll();
                return Results.Ok(result);
            }
            catch(Exception e)
            {
                _log.LogError(e, e.Message);
                return Results.Problem();
            }
        }

        internal async Task<IResult> Query(MovieQuery query)
        {
            try
            {
                var result = await _movieService.Query(query);
                return Results.Ok(result);
            }
            catch (Exception e)
            {
                _log.LogError(e, e.Message);
                return Results.Problem();
            }
        }
    }
}
