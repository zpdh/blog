using Blog.Domain.Entities;

namespace Blog.Domain.Services;

public interface IUserTokenService {
    Task<User> GetUserFromTokenAsync();
}