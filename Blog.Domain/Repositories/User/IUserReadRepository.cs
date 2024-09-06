namespace Blog.Domain.Repositories.User;

public interface IUserReadRepository {
    public Task<bool> UserExistsAsync(string email);
}