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
        public int Year { get; set; }

        public IEntity GetEntity()
        {
            throw new NotImplementedException();
        }
    }
}
