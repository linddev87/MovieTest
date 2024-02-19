namespace Domain.Models
{
    public class MovieQuery
    {
        public MovieQuery(int pageSize = 10, int pageNumber = 1, string? searchPhrase = null, int? from = null, int? to = null)
        {
            From = from;
            To = to;
            PageSize = pageSize;
            PageNumber = pageNumber;
            SearchPhrase = searchPhrase ?? string.Empty;
        }

        public int? From { get; }
        public int? To { get; }
        public int PageSize { get; }
        public int PageNumber { get; }
        public string? SearchPhrase { get; }
    }
}