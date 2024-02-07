using Infrastructure.Repos;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public class GenericRepositoryTests
    {
        [Fact]
        public async Task Can_Get_Movie_List()
        {
            //Arrange
            var context = GetDbContext();
            var repo = new GenericRepository<Movie>(context);
            
            //Act
            var list = await repo.ListAsync();

            //Assert
            Assert.NotNull(list);
            Assert.True(list.Any());
        }

        [Fact]
        public async Task Can_Get_By_Id()
        {
            //Arrange
            var testId = 123456;
            var context = GetDbContext();
            var repo = new GenericRepository<Movie>(context);
            var newMovie1 = new Movie("An Epic Movie 2", 1989);
            var newMovie2 = new Movie("An Epic Movie 3", 1991);
            newMovie2.Id = testId;

            //Act
            await repo.CreateRangeAsync(new List<Movie> { newMovie1, newMovie2 });
            await repo.SaveChangesAsync();
            var movieById = await repo.GetByIdAsync(testId);

            Assert.NotNull(movieById);
            Assert.True(string.Equals(movieById.Name, newMovie2.Name));
            Assert.True(movieById.Year == newMovie2.Year);
        }

        [Fact]
        public async Task Can_Insert_Range()
        {
            //Arrange
            var context = GetDbContext();
            var repo = new GenericRepository<Movie>(context);
            var newMovies = new List<Movie>();

            for(var i = 0; i < 10; i++)
            {
                newMovies.Add(new Movie($"An Epic Movie {i}", 1987 + i));
            }

            //Act
            var preInsertList = await repo.ListAsync();
            await repo.CreateRangeAsync(newMovies);
            await repo.SaveChangesAsync();
            var postInsertList = await repo.ListAsync();

            //Assert
            Assert.True(postInsertList.Count - newMovies.Count == preInsertList.Count);
        }

        [Fact]
        public async Task Can_Create_Movie()
        {
            //Arrange
            var name = Guid.NewGuid().ToString();
            var year = new Random().Next(1950, DateTime.UtcNow.Year);
            var newMovie = new Movie(name, year);
            var context = GetDbContext();
            var repo = new GenericRepository<Movie>(context);

            //Act
            var inserted = await repo.CreateAsync(newMovie);
            await repo.SaveChangesAsync();

            //Assert
            Assert.True(string.Equals(inserted.Name, name));
            Assert.True(inserted.Year == year);
        }

        [Fact]
        public async Task Cannot_Create_Duplicates()
        {
            //Arrange
            var context = GetDbContext();
            var repo = new GenericRepository<Movie>(context);
            var name = "An epic movie";
            var year = 1987;

            //Act
            await repo.CreateAsync(new Movie(name, year));

            //Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await repo.CreateAsync(new Movie(name, year)));
        }

        [Fact]
        public async Task Can_Find_Entity_With_Linq()
        {
            //Arrange
            var newMovie = new Movie("An epic movie", 1987);
            var context = GetDbContext();
            var repo = new GenericRepository<Movie>(context);

            //Act
            await repo.CreateAsync(newMovie);
            await repo.SaveChangesAsync();
            var inserted = await repo.FindAsync(m => m.Name.Contains("epic"));            

            //Assert
            Assert.NotNull(inserted);
            Assert.True(inserted.Count() == 1); 
            Assert.True(string.Equals(inserted.FirstOrDefault()?.Name, newMovie.Name));
        }

        private ApplicationDbContext GetDbContext()
        {
            var movieList = new List<Movie>() { new Movie("Test1", 1950), new Movie("Test2", 1950) };
            var context = new TestApplicationDbContext();

            context.Movies.AddRange(movieList);
            context.SaveChanges();

            return context;
        }
    }
}