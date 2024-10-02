namespace PM.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        Task<int> CompleteAsync();
    }
}
