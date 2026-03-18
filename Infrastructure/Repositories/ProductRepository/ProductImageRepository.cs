using Application.Interfaces.IProductInterface;
using Domain.Entities.ProductEntites;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.ProductRepository
{
    public class ProductImageRepository : BaseRepository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(AppDbContext ctx) : base(ctx)
        {
        }

        public async Task<ProductImage> AddProductImageAsync(ProductImage productImage)
        {
            await _ctx.ProductImages.AddAsync(productImage);
            await _ctx.SaveChangesAsync();
            return productImage;
        }

        public async Task<IEnumerable<ProductImage>> GetByProductIdAsync(int productId)
        {
            return await _ctx.Set<ProductImage>().Where(i => i.ProductId == productId).ToListAsync();
        }
    }
}
