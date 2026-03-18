using Domain.Entities.UserEntities;

namespace Application.Interfaces.IUserInterface
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUsernameAsync(string username);
    }

}
