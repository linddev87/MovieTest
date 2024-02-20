namespace Application.Models
{
    public class MovieQuery
    {
        public MovieQuery(int? pageSize, int? pageNumber, string? searchPhrase = null, int? from = null, int? to = null)
        {
            From = from;
            To = to;
            PageSize = pageSize ?? 10;
            PageNumber = pageNumber ?? 1;
            SearchPhrase = searchPhrase ?? string.Empty;
        }

        public MovieQuery()
        {

        }

        public int? From { get; set; }
        public int? To { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public string? SearchPhrase { get; set; }
    }
}