

public class CsvImportService<TIn, TOut> : IImportService<TIn,TOut> where TIn : IDto where TOut : IEntity{
    private string filePath;
    private readonly IGenericRepository<TOut> _repo;

    public CsvImportService(IGenericRepository<TOut> repo)
    {
        _repo = repo;
        filePath = string.Empty;
    }

    public async Task RunImport(string[]? args)
    {
        if(args is null || args.Length == 0){
            throw new ArgumentNullException("Missing filepath for CSV import");
        }

        filePath = args[0];

        var dtos = GetDtos();
        var entities = dtos.Select(d => d.GetEntity()).ToList();
        if(entities is null){
            throw new InvalidOperationException("Failed to convert Dtos to entitites");
        }

        await InsertNewEntities(entities);
        await ArchiveImportFile();
    }

    private async Task ArchiveImportFile()
    {
        throw new NotImplementedException();
    }

    private async Task InsertNewEntities(List<IEntity> entities)
    {
        var existingDict = await _repo.DictByAltKey();
        var newEntities = entities.Where(e => !existingDict.ContainsKey(e.AlternateKey));

        await _repo.CreateRangeAsync(entities);
        await _repo.SaveChangesAsync();
    }

    private List<TIn> GetDtos(){
        using var streamReader = new StreamReader(filePath);
        using var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture);

        var records = csv.GetRecords<TIn>().ToList();
        return records ?? new List<TIn>();
    }
}