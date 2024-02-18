using Castle.Core.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Moq;
using System.Net;

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

            await context.AddRangeAsync(movieList);
            await context.SaveChangesAsync();

            //Act
            //Source: https://stackoverflow.com/questions/71323013/get-a-response-value-out-of-an-iresult-in-asp-nets-minimal-api
            //According to StackOverflow the simplest way to test the IResult used in Minimal API's is:
            //1. Create 'mock' HttpContext
            var httpContext = GetDefaultHttpContext();

            //2. Pass the mock HttpContext to the IResult.ExecuteAsync() method. This will write the response body to the HttpContext.
            var result = await movieService.ListAll();
            await result.ExecuteAsync(httpContext);

            //3.Read the response from the mock HttpContext and deserialize it into whatever we expect the service to return.
            httpContext.Response.Body.Position = 0;
            var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            var responseBody = await JsonSerializer.DeserializeAsync<List<Movie>>(httpContext.Response.Body, jsonOptions);

            //Assert
            Assert.Equal(200, httpContext.Response.StatusCode);
            Assert.True(responseBody?.Count == movieList.Count);
        }

        [Fact]
        public async Task Query_Returns_GenericQueryResult()
        {
            //Arrange
            var movieList = new List<Movie>() { new("An Epic Movie 6", 1992), new("An Epic Movie 7", 1994) };
            var serviceProvider = GetServiceCollection().BuildServiceProvider().CreateScope().ServiceProvider;
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var movieService = serviceProvider.GetRequiredService<MovieService>();

            await context.AddRangeAsync(movieList);
            await context.SaveChangesAsync();

            //Act
            //Source: https://stackoverflow.com/questions/71323013/get-a-response-value-out-of-an-iresult-in-asp-nets-minimal-api
            //According to StackOverflow the simplest way to test the IResult used in Minimal API's is:
            //1. Create 'mock' HttpContext
            var httpContext = GetDefaultHttpContext();

            //2. Pass the mock HttpContext to the IResult.ExecuteAsync() method. This will write the response body to the HttpContext.
            var result = await movieService.Query(new MovieQueryRequest());
            await result.ExecuteAsync(httpContext);

            //3.Read the response from the mock HttpContext and deserialize it into whatever we expect the service to return.
            httpContext.Response.Body.Position = 0;
            var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web) { PropertyNameCaseInsensitive = true};
            var responseBody = await JsonSerializer.DeserializeAsync<GenericQueryResult<Movie>>(httpContext.Response.Body, jsonOptions);

            //Assert
            Assert.Equal(200, httpContext.Response.StatusCode);
            Assert.False(responseBody is null);
            Assert.True(responseBody?.Entities.Count() == movieList.Count);
        }

        [Fact]
        public async Task Endpoints_Return_500_On_Exceptions()
        {
            //Arrange
            var serviceProvider = GetServiceCollection().BuildServiceProvider().CreateScope().ServiceProvider;
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var movieService = serviceProvider.GetRequiredService<MovieService>();

            //Act
            await context.DisposeAsync(); //Dispose context to cause exception
            var listAllResult = await movieService.ListAll() as IStatusCodeHttpResult;
            var queryResult = await movieService.Query(new MovieQueryRequest()) as IStatusCodeHttpResult;

            //Assert
            Assert.Equal(500, listAllResult?.StatusCode);         
            Assert.Equal(500, queryResult?.StatusCode);         
        }

        private HttpContext GetDefaultHttpContext()
        {
            return new DefaultHttpContext
            {
                RequestServices = new ServiceCollection().AddLogging().BuildServiceProvider(),
                Response =
                {
                    Body = new MemoryStream(),
                },
            };
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