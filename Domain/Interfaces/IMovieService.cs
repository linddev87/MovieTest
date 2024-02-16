﻿namespace Domain.Interfaces{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> ListAll();
        Task<IQueryResult<Movie>> Query(int from, int to, string searchPhrase, int pageSize, int pageNumber);
    }    
}
