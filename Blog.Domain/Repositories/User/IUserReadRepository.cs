namespace Blog.Domain.Repositories.User;

public interface IUserReadRepository {
    public Task<bool> UserExistsAsync(string email);
    public Task<Entities.User?> GetUserByEmailAsync(string email);
}