using PM.DbContextt;

namespace PM.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IProductRepository _productRepository;

        public UnitOfWork(ApplicationDbContext context, IProductRepository productRepository)
        {
            _context = context;
        }


        public IProductRepository ProductRepository => _productRepository ?? new ProductRepository(_context);
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
