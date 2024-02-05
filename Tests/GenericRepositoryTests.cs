using Infrastructure.Repos;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public class GenericRepositoryTests
    {
        [Fact]
        public void Can_Get_Movie_List()
        {
            //Arrange
            var movieList = new List<Movie>() { new Movie(), new Movie() };
            var context = new TestApplicationDbContext();
            var repo = new GenericRepository<Movie>(context);
           
            //Act
            context.Movies.AddRange(movieList);
            context.SaveChanges();
            var list = repo.List();

            //Assert
            Assert.NotNull(list);
            Assert.Equal(movieList.Count, list.Count);
        }
    }
}