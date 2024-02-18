using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    internal static class TestUtilities
    {
        private static ApplicationDbContext GetDbContext(string dbName)
        {
            var movieList = new List<Movie>() { new Movie("DefaultTestTitle 1", 1950), new Movie("DefaultTestTitle 2", 1950) };
            var context = new TestApplicationDbContext(dbName);

            context.Movies.AddRange(movieList);
            context.SaveChanges();

            return context;
        }

        public static MovieRepository GetNewMovieRepository()
        {
            var context = GetDbContext(Guid.NewGuid().ToString());
            return new MovieRepository(context);
        }

        public static GenericRepository<T> GetNewGenericRepository<T>() where T : class, IEntity
        {
            var context = GetDbContext(Guid.NewGuid().ToString());
            return new GenericRepository<T>(context);
        }
    }
}
