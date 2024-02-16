namespace Domain.Models
{
    public class MovieResult : IEntityResult
    {
        public int Id { get; }
        public string Title { get; }
        public int Year { get; }

        public MovieResult(Movie movie)
        {
            Id = movie.Id;
            Title = movie.Title;
            Year = movie.Year;
        }
    }
}
