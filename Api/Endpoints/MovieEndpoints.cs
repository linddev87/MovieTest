
namespace Api.Endpoints{
    public class MovieEndpoints
    {
        public static void Map(WebApplication app){
            app.MapGet("/movies", GetAllMovies());
            app.MapGet("/movies/query", QueryMovies());
        }

        private static RequestDelegate QueryMovies()
        {
            throw new NotImplementedException();
        }

        private static RequestDelegate GetAllMovies()
        {
            throw new NotImplementedException();
        }
    }
};

