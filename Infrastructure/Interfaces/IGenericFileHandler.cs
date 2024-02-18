namespace Infrastructure.Interfaces
{
    public interface IGenericFileHandler
    {
        static abstract Task<IEnumerable<T>> GetRecordsFromFile<T>(string filePath);

        static abstract void ArchiveImportFile(string filePath);
    }
}