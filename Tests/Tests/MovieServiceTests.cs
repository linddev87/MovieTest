

namespace Tests.Tests
{
    public class MovieServiceTests
    {
        [Fact]
        public async Task List_All_Returns_List_Of_Movies()
        {
            //Arrange
            var movieList = new List<Movie>() { new("An Epic Movie 6", 1992), new("An Epic Movie 7", 1994) };
            var serviceProvider = GetServiceCollection().BuildServiceProvider().CreateScope().ServiceProvider;
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var movieService = serviceProvider.GetRequiredService<MovieService>();

            //Act
            await context.AddRangeAsync(movieList);
            await context.SaveChangesAsync();

            var result = await movieService.ListAll();

            //Assert
            Assert.False(result is null);
            Assert.True(result.Count() == movieList.Count);
        }

        [Fact]
        public async Task Query_Returns_GenericQueryResult()
        {
            //Arrange
            var movieList = new List<Movie>() { new("An Epic Movie 6", 1992), new("An Epic Movie 7", 1994) };
            var serviceProvider = GetServiceCollection().BuildServiceProvider().CreateScope().ServiceProvider;
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var movieService = serviceProvider.GetRequiredService<MovieService>();

            //Act
            await context.AddRangeAsync(movieList);
            await context.SaveChangesAsync();
            var result = await movieService.Query(new MovieQuery());
            
            //Assert
            Assert.False(result is null);
            Assert.True(result.Entities.Count() == movieList.Count);
        }

        private ServiceCollection GetServiceCollection()
        {
            var collection = new ServiceCollection();
            collection.AddLogging();
            collection.AddMemoryCache();
            collection.AddDbContext<ApplicationDbContext, TestApplicationDbContext>();
            collection.AddScoped<MovieService>();
            collection.AddScoped<IMovieRepository, MovieRepository>();

            return collection;
        }
    }
}