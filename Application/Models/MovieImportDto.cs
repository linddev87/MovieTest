namespace Application.Models
{
    public class MovieImportDto : IDto
    {
        public string Title { get; set; }
        public int Year { get; set; }

        public IEntity GetEntity()
        {
            return new Movie(Title, Year);
        }
    }
}