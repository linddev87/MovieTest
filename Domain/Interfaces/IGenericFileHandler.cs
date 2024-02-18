namespace Domain.Interfaces
{
    public interface IGenericFileHandler
    {
        abstract Task<IEnumerable<T>> GetRecordsFromFile<T>(string filePath);

        void ArchiveImportFile(string filePath);
    }
}