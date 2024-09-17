namespace Blog.Domain.Repositories.User;

public interface IUserReadRepository {
    public Task<bool> UserWithEmailExistsAsync(string email);
    public Task<bool> UserWithIdExistsAsync(Guid userId);
    public Task<Entities.User?> GetUserByEmailAsync(string email);
    public Task<Entities.User?> GetUserByIdAsync(Guid id);
}