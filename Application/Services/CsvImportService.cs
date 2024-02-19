namespace Application.Services
{
    /// <summary>
    /// Implementation of the IGenericImportService interface that can import Dtos from a CSV file, get the associated Entities and insert missing entities via an implementation of IGenericRepository<T>.
    /// Catches and logs exceptions that occur during the import.
    /// </summary>
    /// <typeparam name="TIn">Expected Data Transfer Object (implementation of IDto)</typeparam>
    /// <typeparam name="TOut">Expected Output Entity (implementation of IEntity)</typeparam>
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

        public async Task<int> RunImport()
        {
            try
            {
                var dtos = await _fileHandler.GetRecordsFromFile<TIn>(_filePath);
                var entities = dtos.Select(d => d.GetEntity()).ToList() ?? throw new InvalidOperationException("Failed to convert Dtos to entitites");
                var insertedCount = await InsertNewEntities(entities);

                _fileHandler.ArchiveImportFile(_filePath);

                return insertedCount;
            }
            catch (Exception e)
            {
                _log.LogError(e, e.Message);
                throw;
            }
        }

        private async Task<int> InsertNewEntities(List<IEntity> entities)
        {
            var existingDict = await _repo.DictByAltKey();
            var newEntities = entities.Where(e => !existingDict.ContainsKey(e.AlternateKey));

            if (!newEntities.Any())
            {
                return 0;
            }

            await _repo.CreateRangeAsync(newEntities);
            await _repo.SaveChangesAsync();

            return newEntities.Count();
        }
    }
}