namespace Infrastructure.Services
{
    public class FileHandler : IGenericFileHandler
    {
        public static async Task<IEnumerable<T>> GetRecordsFromFile<T>(string filePath)
        {
            using var streamReader = new StreamReader(filePath);
            using var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture);

            var records = await csv.GetRecordsAsync<T>().ToListAsync();
            return records ?? new List<T>();
        }

        public static void ArchiveImportFile(string filePath)
        {
            var lastSlashIndex = filePath.LastIndexOf('/');

            var folder = filePath.Substring(0, lastSlashIndex);
            var fileName = filePath.Substring(lastSlashIndex + 1).Split('.');

            var archiveFileName = $"{fileName[0]}_ImportedAt_{DateTime.UtcNow.ToString("dd_M_yyyy-HH_mm_ss")}.{fileName[1]}";
            var archivePath = $"{folder}/Archive";

            Directory.CreateDirectory(archivePath);
            File.Move(filePath, $"{archivePath}/{archiveFileName}");
        }
    }
}