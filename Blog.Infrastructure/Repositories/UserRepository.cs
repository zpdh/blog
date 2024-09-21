using Blog.Domain.Entities;
using Blog.Domain.Repositories.User;
using Blog.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories;

public class UserRepository(DataContext context) : IUserReadRepository, IUserWriteRepository {
    public async Task<bool> UserWithEmailExistsAsync(string email) {
        return await context.Users
            .AnyAsync(user => user.Email == email);
    }

    public async Task<bool> UserWithIdExistsAsync(Guid userId) {
        return await context.Users
            .AnyAsync(user => user.Id == userId);
    }

    public async Task<User?> GetUserByEmailAsync(string email) {
        return await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Email == email);
    }

    public async Task<User?> GetUserByIdAsync(Guid id) {
        return await context.Users
            .FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task AddUserAsync(User user) {
        await context.Users
            .AddAsync(user);
    }

    public void UpdateUser(User user) {
        context.Users.Update(user);
    }
}