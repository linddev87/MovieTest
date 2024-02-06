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
        public async Task Can_Create_Movie()
        {
            var name = Guid.NewGuid().ToString();
            var year = new Random().Next(1950, DateTime.UtcNow.Year);
            var newMovie = new Movie(name, year);

            var context = GetDbContext();
            var repo = new GenericRepository<Movie>(context);

            var inserted = await repo.CreateAsync(newMovie);
            await repo.SaveChangesAsync();

            ass
        }

        private ApplicationDbContext GetDbContext()
        {
            var movieList = new List<Movie>() { new Movie(), new Movie() };
            var context = new TestApplicationDbContext();

            context.Movies.AddRange(movieList);
            context.SaveChanges();

            return context;
        }
    }
}