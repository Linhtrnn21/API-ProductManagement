using PM.DbContextt;
using PM.Entities;
using PM.Repository.Common;

namespace PM.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
