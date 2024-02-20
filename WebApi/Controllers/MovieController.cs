namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly ILogger<MovieController> _log;

        public MovieController(IMovieService movieService, ILogger<MovieController> log)
        {
            _log = log;
            _movieService = movieService;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<Movie>>> ListAll()
        {
            try
            {
                var result = await _movieService.ListAll();
                return Ok(result);
            }
            catch (Exception e)
            {
                _log.LogError(e, e.Message);
                return Problem();
            }
        }

        [HttpGet("Query")]
        public async Task<ActionResult<GenericQueryResult<Movie>>> Query([FromQuery] MovieQuery query)
        {
            try
            {
                var result = await _movieService.Query(query);
                return Ok(result);
            }
            catch (Exception e)
            {
                _log.LogError(e, e.Message);
                return Problem();
            }
        }
    }
}
