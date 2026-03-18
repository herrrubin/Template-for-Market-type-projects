using Application.Interfaces.IProductInterface;
using Domain.Entities.ProductEntites;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Repositories.ProductRepository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext ctx) : base(ctx)
        {
        }
    }
}
