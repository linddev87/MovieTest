using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class MovieQueryRequest
    {
        public MovieQueryRequest(int from, int to, int pageSize, int pageNumber, string searchPhrase)
        {
            From = from;
            To = to;
            PageSize = pageSize;
            PageNumber = pageNumber;
            SearchPhrase = searchPhrase;
        }

        public int To { get; }
        public int From { get; }
        public string SearchPhrase { get; }
        public int PageSize { get; }
        public int PageNumber { get; }
    }
}
