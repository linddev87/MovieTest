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

        public async Task<IResult> ListAll()
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

        public async Task<IResult> Query(MovieQuery query)
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
        
        public async Task<IResult> Query(int? pageSize, int? pageNumber, string? searchPhrase, int? from, int? to)
        {
            try
            {
                var query = new MovieQuery(pageSize, pageNumber, searchPhrase, from, to);

                var result = await _movieService.Query(query);
                return Results.Ok(result);
            }
            catch (Exception e)
            {
                _log.LogError(e, e.Message);
                return Results.Problem();
            }
        }

        internal async Task<IResult> Query(HttpContext context)
        {
            try
            {
                var q = context.Request.Query;
                var pageSize = q["pageSize"].FirstOrDefault();
                var pageNumber = q["pageNumber"].FirstOrDefault();
                var to = q["to"].FirstOrDefault();
                var from = q["from"].FirstOrDefault();
                var searchPhrase = q["searchPhrase"].FirstOrDefault();

                var query = new MovieQuery(pageSize: pageSize, pageNumber: pageNumber, to: to, from: from, searchPhrase: searchPhrase);

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
