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
        public string Name { get; set; }
        public int Year { get; set; }

        public Movie(string name, int year)
        {
            Name = name;
            Year = year;
        }

        public Movie()
        {
            Name = string.Empty; 
            Year = 0;
        }
    }
}
