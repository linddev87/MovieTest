namespace Application.Interfaces
{
    public interface IGenericImportService<TIn, TOut> where TIn : IDto where TOut : IEntity
    {
        public Task<int> RunImport();
    }
}