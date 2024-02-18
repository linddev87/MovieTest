namespace Tests
{
    internal static class TestUtilities
    {
        private static TestApplicationDbContext GetDbContext()
        {
            var movieList = new List<Movie>() { new Movie("DefaultTestTitle 1", 1950), new Movie("DefaultTestTitle 2", 1950) };
            var context = new TestApplicationDbContext();

            context.Movies.AddRange(movieList);
            context.SaveChanges();

            return context;
        }

        public static MovieRepository GetNewMovieRepository()
        {
            var context = GetDbContext();
            return new MovieRepository(context);
        }

        public static GenericRepository<T> GetNewGenericRepository<T>() where T : class, IEntity
        {
            var context = GetDbContext();
            return new GenericRepository<T>(context);
        }
    }
}
