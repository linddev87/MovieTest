using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class MovieImportDto : IDto
    {
        public required string Title { get; set; }
        public required int Year { get; set; }

        public IDto Create(Dictionary<string, object> props)
        {
            if(!props.ContainsKey("title") || !props.ContainsKey("year"))
            {
                throw new ArgumentNullException(string.Format("Props dict is missing the required keys."));
            }

            var title = props["Title"].ToString();
            var year = (int)props["Year"];

            if (string.IsNullOrEmpty(title) || year == 0)
            {
                throw new InvalidOperationException("Could not build MovieImportDto from the data provided.");
            }

            return new MovieImportDto() { Title = title, Year = year };
        }
    }
}
