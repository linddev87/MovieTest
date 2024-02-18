namespace Application.Services
{
    public class CsvImportService<TIn, TOut> : IGenericImportService<TIn, TOut> where TIn : IDto where TOut : IEntity
    {
        private readonly string _filePath;
        private readonly IGenericRepository<TOut> _repo;
        private readonly ILogger<CsvImportService<TIn, TOut>> _log;
        private readonly IGenericFileHandler _fileHandler;

        public CsvImportService(IGenericRepository<TOut> repo, IConfiguration config, ILogger<CsvImportService<TIn, TOut>> log, IGenericFileHandler fileHandler)
        {
            _repo = repo;
            _log = log;
            _filePath = config["PathToImportFile"] ?? throw new InvalidOperationException("Missing configuration value 'PathToImportFile'");
            _fileHandler = fileHandler;
        }

        public async Task RunImport()
        {
            try
            {
                var dtos = await _fileHandler.GetRecordsFromFile<TIn>(_filePath);
                var entities = dtos.Select(d => d.GetEntity()).ToList() ?? throw new InvalidOperationException("Failed to convert Dtos to entitites");
                await InsertNewEntities(entities);
                _fileHandler.ArchiveImportFile(_filePath);
            }
            catch (Exception e)
            {
                _log.LogError(e, e.Message);
                throw;
            }
        }

        private async Task InsertNewEntities(List<IEntity> entities)
        {
            var existingDict = await _repo.DictByAltKey();
            var newEntities = entities.Where(e => !existingDict.ContainsKey(e.AlternateKey));

            if (!newEntities.Any())
            {
                return;
            }

            await _repo.CreateRangeAsync(newEntities);
            await _repo.SaveChangesAsync();
        }
    }
}