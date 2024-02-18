namespace Application.Services
{
    public class CsvImportService<TIn, TOut> : IGenericImportService<TIn, TOut> where TIn : IDto where TOut : IEntity
    {
        private string _filePath;
        private readonly IGenericRepository<TOut> _repo;
        private readonly ILogger<CsvImportService<TIn, TOut>> _log;

        public CsvImportService(IGenericRepository<TOut> repo, IConfiguration config, ILogger<CsvImportService<TIn, TOut>> log)
        {
            _repo = repo;
            _log = log;
            _filePath = config["PathToImportFile"] ?? throw new InvalidOperationException("Missing configuration value 'PathToImportFile'");
        }

        public async Task RunImport()
        {
            try
            {
                var dtos = await FileHandler.GetRecordsFromFile<TIn>(_filePath);
                var entities = dtos.Select(d => d.GetEntity()).ToList();
                if (entities is null)
                {
                    throw new InvalidOperationException("Failed to convert Dtos to entitites");
                }

                await InsertNewEntities(entities);
                FileHandler.ArchiveImportFile(_filePath);
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