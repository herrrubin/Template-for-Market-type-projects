using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T>
        where T : BaseEntity
    {
        protected readonly AppDbContext _ctx;
        protected readonly DbSet<T> _dbSet;

        protected BaseRepository(AppDbContext ctx)
        {
            _ctx = ctx;
            _dbSet = ctx.Set<T>();
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var count = await _dbSet.Where(x => x.Id == id).ExecuteDeleteAsync();
            return count > 0;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<T> SaveAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<int> GetAllCountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public virtual async Task<bool> IsExists(int id)
        {
            return await _dbSet.AnyAsync(x => x.Id == id);
        }

        public virtual async Task<PaginatedResponse<T>> GetAllWithFilterAsync(
            PaginationRequest pagination,
            BaseFilter? filter = null,
            Dictionary<string, object>? additionalParams = null
        )
        {
            pagination.Normalize();
            var query = _dbSet.AsNoTracking().AsQueryable();

            query = ApplyFilter(query, filter);
            query = ApplyAdditionalFilters(query, additionalParams);

            var itemsCount = await query.CountAsync();

            var items = await query.Skip(pagination.Offset).Take(pagination.Limit).ToListAsync();

            return new PaginatedResponse<T> { ItemsCount = itemsCount, Items = items };
        }

        public virtual async Task<int> GetAllCountWithFilterAsync(
            BaseFilter? filter = null,
            Dictionary<string, object>? additionalParams = null
        )
        {
            var query = _dbSet.AsNoTracking().AsQueryable();
            query = ApplyFilter(query, filter);
            query = ApplyAdditionalFilters(query, additionalParams);
            return await query.CountAsync();
        }


        protected virtual IQueryable<T> ApplyFilter(IQueryable<T> query, BaseFilter? filter)
        {
            return query;
        }

        protected virtual IQueryable<T> ApplyAdditionalFilters(
            IQueryable<T> query,
            Dictionary<string, object>? additionalParams = null
        )
        {
            return query;
        }
    }
}
