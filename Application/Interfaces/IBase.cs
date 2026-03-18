using Application.DTOs;

namespace Application.Interfaces
{
    public interface IBaseSaver<T>
        where T : class
    {
        Task<T> SaveAsync(T entity);
        Task<T> UpdateAsync(T entity);
    }

    public interface IBaseDeleter<T>
    {
        Task<bool> DeleteAsync(int id);
    }

    public interface IBaseReader<T>
        where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<int> GetAllCountAsync();
        Task<T?> GetByIdAsync(int id);
        Task<bool> IsExists(int id);

        Task<PaginatedResponse<T>> GetAllWithFilterAsync(
            PaginationRequest pagination,
            BaseFilter? filter = null,
            Dictionary<string, object>? additionalParams = null
        );
        Task<int> GetAllCountWithFilterAsync(
            BaseFilter? filter = null,
            Dictionary<string, object>? additionalParams = null
        );
    }

    // Составной интерфейс, объединяющий все операции
    public interface IBaseRepository<T> : IBaseSaver<T>, IBaseDeleter<T>, IBaseReader<T>
        where T : class
    { }
}

