namespace Blog.Domain.Repositories.User;

public interface IUserWriteRepository {
    public Task AddUserAsync(Entities.User user);

    public void UpdateUser(Entities.User user);
}