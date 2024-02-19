using Domain.Models;
using NuGet.Frameworks;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Tests.Tests
{
    public class MovieRepositoryTests
    {
        [Fact]
        public async Task Can_Query_By_Year()
        {
            //Arrange
            var repo = TestUtilities.GetNewMovieRepository();
            var newMovie = new Movie("An Epic Movie 4", 1997);
            var includeQuery = new MovieQuery(from: 1996, to: 1998);
            var excludeQuery = new MovieQuery(to: 1996);

            //Act
            var inserted = await repo.CreateAsync(newMovie);
            await repo.SaveChangesAsync();

            var shouldFindNewMovie = await repo.Query(includeQuery);
            var shouldNotFindNewMovie = await repo.Query(excludeQuery);

            //Assert
            Assert.Contains(shouldFindNewMovie.Entities, e => e.Id == inserted.Id);
            Assert.DoesNotContain(shouldNotFindNewMovie.Entities, e => e.Id == inserted.Id);
        }

        [Fact]
        public async Task Can_Query_By_Title()
        {
            //Arrange
            var repo = TestUtilities.GetNewMovieRepository();
            var newMovie = new Movie("An Epic Movie 11", 2020);
            var includeQuery = new MovieQuery(searchPhrase: "Epic Movie");
            var excludeQuery = new MovieQuery(searchPhrase: "Insane Movie");

            //Act
            var inserted = await repo.CreateAsync(newMovie);
            await repo.SaveChangesAsync();

            var shouldFindNewMovie = await repo.Query(includeQuery);
            var shouldNotFindNewMovie = await repo.Query(excludeQuery);

            //Assert
            Assert.Contains(shouldFindNewMovie.Entities, e => e.Id == inserted.Id);
            Assert.DoesNotContain(shouldNotFindNewMovie.Entities, e => e.Id == inserted.Id);
        }

        [Fact]
        public async Task Query_By_Title_Ignores_Case()
        {
            //Arrange
            var repo = TestUtilities.GetNewMovieRepository();
            var newMovie = new Movie("An Epic Movie 12", 2021);
            var pascalCaseQuery = new MovieQuery(searchPhrase: "Epic Movie");
            var lowerCaseQuery = new MovieQuery(searchPhrase: "epic movie");

            //Act
            var inserted = await repo.CreateAsync(newMovie);
            await repo.SaveChangesAsync();

            var shouldFindWithPascalCase = await repo.Query(pascalCaseQuery);
            var shouldFindWithLowerCase = await repo.Query(lowerCaseQuery);

            //Assert
            Assert.Contains(shouldFindWithPascalCase.Entities, e => e.Id == inserted.Id);
            Assert.Contains(shouldFindWithLowerCase.Entities, e => e.Id == inserted.Id);
        }

        [Fact]
        public async Task Query_Result_Has_Default_Pagination()
        {
            //Arrange
            var repo = TestUtilities.GetNewMovieRepository();
            var preInsertCount = repo.ListAsync().Result.Count;
            var movieList = new List<Movie>();
            
            for(var i = 0; i < 100; i++)
            {
                movieList.Add(new Movie($"PaginationTest{i}", 1920 + i));
            }
            
            var totalCount = preInsertCount + movieList.Count;

            //Act
            await repo.CreateRangeAsync(movieList);
            await repo.SaveChangesAsync();
            var queryResult = await repo.Query(new MovieQuery());

            //Assert
            Assert.Equal(queryResult.EntityCount, totalCount);
            Assert.Equal(queryResult.Entities.Count(), queryResult.PageSize);
            Assert.Equal(queryResult.PageNumber, queryResult.PageNumber);
            Assert.Equal(queryResult.PageCount, (int)Math.Ceiling((decimal)totalCount / queryResult.PageSize));
        }

        [Fact]
        public async Task Can_Query_Specific_Pages()
        {
            //Arrange
            var repo = TestUtilities.GetNewMovieRepository();
            var requiredPageSize = 20;
            var movieList = new List<Movie>();

            for (var i = 0; i < 100; i++)
            {
                movieList.Add(new Movie($"PaginationTest{i}", 1920 + i));
            }

            //Act
            await repo.CreateRangeAsync(movieList);
            await repo.SaveChangesAsync();

            var queryPage1Result = await repo.Query(new MovieQuery(pageSize: requiredPageSize));
            var queryPage2Result = await repo.Query(new MovieQuery(pageSize: requiredPageSize, pageNumber: 2));
            
            var page1Ids = queryPage1Result.Entities.Select(e => e.Id);
            var page2Ids = queryPage2Result.Entities.Select(e => e.Id);

            //Assert
            Assert.True(!page1Ids.Intersect(page2Ids).Any());
            Assert.True(page1Ids.Count() == requiredPageSize && page2Ids.Count() == requiredPageSize);
        }

        [Fact]
        public async Task Query_Result_Returns_Parameters()
        {
            //Arrange
            var repo = TestUtilities.GetNewMovieRepository();
            var newMovie = new Movie("An Epic Movie 13", 2022);
            var searchPhrase = "Epic";
            var from = 1950;
            var to = 2024;

            //Act
            await repo.CreateAsync(newMovie);
            await repo.SaveChangesAsync();

            var queryResult = await repo.Query(new MovieQuery(searchPhrase: searchPhrase, from: from, to: to));

            //Assert
            Assert.True(queryResult.Parameters.ContainsKey("SearchPhrase") && queryResult.Parameters.ContainsKey("From") && queryResult.Parameters.ContainsKey("To"));
            Assert.Equal(queryResult.Parameters["SearchPhrase"], searchPhrase);
            Assert.Equal(queryResult.Parameters["From"], from);
            Assert.Equal(queryResult.Parameters["To"], to);
        }


    }
}
