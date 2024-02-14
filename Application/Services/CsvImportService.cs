using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using CsvHelper;
using Domain.Entities;
using Infrastructure.Repos;

public class CsvImportService<TIn, TOut> : IImportService<TIn,TOut> where TIn : IDto where TOut : IEntity{
    private string filePath;
    private readonly IGenericRepository<Movie> _repo;

    public CsvImportService(IGenericRepository<Movie> repo)
    {
        _repo = repo;
    }

    public async Task RunImport(string[]? args)
    {
        if(args is null || args.Length == 0){
            throw new ArgumentNullException("Missing filepath for CSV import");
        }

        filePath = args[0];

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
        using var streamReader = new StreamReader(filePath);
        using var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture);

        var records = csv.GetRecords<TIn>().ToList();
        return records ?? new List<TIn>();
    }
}