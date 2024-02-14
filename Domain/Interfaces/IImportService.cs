public interface IImportService<TIn, TOut> where TIn : IDto where TOut : IEntity{
    public Task RunImport(string[]? args);
}