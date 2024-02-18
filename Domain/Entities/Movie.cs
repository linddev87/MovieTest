namespace Domain.Entities
{
    public class Movie : IEntity
    {
        [Key]
        public int Id { get; set; }

        [JsonIgnore]
        public string AlternateKey { get; set; }

        [JsonIgnore]
        public string TitleToLower { get; set; }

        public string Title { get; set; }
        public int Year { get; set; }

        [JsonIgnore]
        public DateTime CreatedDate { get; set; }

        public Movie(string title, int year)
        {
            Title = title;
            TitleToLower = title.ToLowerInvariant();
            Year = year;
            AlternateKey = $"{year}-{title}";
            CreatedDate = DateTime.UtcNow;
        }
    }
}