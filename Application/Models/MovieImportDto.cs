using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class MovieImportDto
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public MovieImportDto(string title, int year)
        {
            Title = title;
            Year = year;
        }
    }
}
