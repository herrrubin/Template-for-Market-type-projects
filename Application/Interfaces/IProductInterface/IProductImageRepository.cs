using Domain.Entities.ProductEntites;

namespace Application.Interfaces.IProductInterface
{
    public interface IProductImageRepository : IBaseRepository<ProductImage>
    {
        Task<ProductImage> AddProductImageAsync(ProductImage productImage);
        Task<IEnumerable<ProductImage>> GetByProductIdAsync(int productId);
    }
}
