using System.Globalization;
using System.Security.Claims;
using Application.Models;
using CsvHelper;
using Domain.Entities;
using Infrastructure.Repos;

public class CsvImportService<TIn, TOut> where TIn : IDto where TOut : IEntity{
    private readonly string _filePath;
    private readonly GenericRepository<Movie> _repo;

    public CsvImportService(string filePath, GenericRepository<Movie> repo)
    {
        _filePath = filePath;
        _repo = repo;
    }

    public async Task RunFileImport()
    {
        var dtos = GetDtos();
        var entities = dtos.Select(d => d.GetEntity()).ToList();
        await InsertNewEntities(entities);
        await ArchiveImportFile();
    }

    private async Task ArchiveImportFile()
    {
        throw new NotImplementedException();
    }

    private async Task InsertNewEntities(List<IEntity> entities)
    {
        throw new NotImplementedException();
    }

    private List<TIn> GetDtos(){
        using var streamReader = new StreamReader(_filePath);
        using var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture);

        var records = csv.GetRecords<TIn>().ToList();
        return records ?? new List<TIn>();
    }
}