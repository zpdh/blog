using Blog.Domain.Entities;
using Blog.Domain.Repositories.User;
using Blog.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories;

public class UserRepository(DataContext context) : IUserReadRepository, IUserWriteRepository {
    public async Task<bool> UserExists(string email) {
        return await context.Users
            .AnyAsync(user => user.Email == email);
    }
}