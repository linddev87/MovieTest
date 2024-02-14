using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Movie : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string AlternateKey {get; set;}
        public string Title { get; set; }
        public int Year { get; set; }

        public Movie(string title, int year)
        {
            Title = title;
            Year = year;
            AlternateKey = $"{year}-{title}";
        }
    }
}
