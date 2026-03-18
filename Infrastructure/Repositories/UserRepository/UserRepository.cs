using Application.DTOs;
using Application.Interfaces.IUserInterface;
using Domain.Entities.UserEntities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.UserRepository;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext ctx)
        : base(ctx) { }

    public Task<User?> GetByEmailAsync(string email)
    {
        return _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        return _dbSet.FirstOrDefaultAsync(u => u.UserName == username);
    }

    protected override IQueryable<User> ApplyFilter(IQueryable<User> query, BaseFilter? filter)
    {
        // Для User фильтрация не требуется, возвращаем исходный запрос
        return query;
    }
}
