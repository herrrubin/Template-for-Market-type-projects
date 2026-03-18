using Application.Interfaces.IProductInterface;
using Domain.Entities.ProductEntites;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Repositories.ProductRepository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext ctx)
        : base(ctx) { }
    }
}
