using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        Task<IQueryResult<Movie>> Query(MovieQueryRequest req);
    }
}
