
namespace Tests.Tests
{
    public class GenericRepositoryTests
    {
        [Fact]
        public async Task Can_Get_Movie_List()
        {
            //Arrange
            var repo = TestUtilities.GetNewGenericRepository<Movie>();

            //Act
            var list = await repo.ListAsync();

            //Assert
            Assert.NotNull(list);
            Assert.True(list.Count > 0);
        }

        [Fact]
        public async Task Can_Get_By_Id()
        {
            //Arrange
            var repo = TestUtilities.GetNewGenericRepository<Movie>();
            var testId = 123456;
            var newMovie1 = new Movie("An Epic Movie 2", 1989);
            var newMovie2 = new Movie("An Epic Movie 3", 1991)
            {
                Id = testId
            };

            //Act
            await repo.CreateRangeAsync(new List<Movie> { newMovie1, newMovie2 });
            await repo.SaveChangesAsync();
            var movieById = await repo.GetByIdAsync(testId);

            Assert.NotNull(movieById);
            Assert.True(string.Equals(movieById.Title, newMovie2.Title));
            Assert.True(movieById.Year == newMovie2.Year);
        }

        [Fact]
        public async Task Can_Insert_Range()
        {
            //Arrange
            var repo = TestUtilities.GetNewGenericRepository<Movie>();
            var newMovies = new List<Movie>();

            for (var i = 0; i < 10; i++)
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
            var repo = TestUtilities.GetNewGenericRepository<Movie>();
            var name = Guid.NewGuid().ToString();
            var year = new Random().Next(1950, DateTime.UtcNow.Year);
            var newMovie = new Movie(name, year);

            //Act
            var inserted = await repo.CreateAsync(newMovie);
            await repo.SaveChangesAsync();

            //Assert
            Assert.True(string.Equals(inserted.Title, name));
            Assert.True(inserted.Year == year);
        }

        [Fact]
        public async Task Cannot_Create_Duplicates()
        {
            //Arrange
            var repo = TestUtilities.GetNewGenericRepository<Movie>();
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
            var repo = TestUtilities.GetNewGenericRepository<Movie>();
            var newMovie = new Movie("An epic movie", 1987);

            //Act
            await repo.CreateAsync(newMovie);
            await repo.SaveChangesAsync();
            var inserted = await repo.FindAsync(m => m.Title.Contains("epic"));

            //Assert
            Assert.NotNull(inserted);
            Assert.True(inserted.Count == 1);
            Assert.True(string.Equals(inserted.FirstOrDefault()?.Title, newMovie.Title));
        }

        [Fact]
        public async Task Can_Get_Dict_By_Alt_Key()
        {
            //Arrange
            var repo = TestUtilities.GetNewGenericRepository<Movie>();
            var newMovie = new Movie("An epic movie", 1987);

            //Act
            await repo.CreateAsync(newMovie);
            await repo.SaveChangesAsync();
            var dict = await repo.DictByAltKey();

            //Assert
            Assert.True(dict.Count > 1);
            Assert.True(dict.ContainsKey(newMovie.AlternateKey));
        }


    }
}