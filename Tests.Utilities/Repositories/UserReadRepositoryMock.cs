using Blog.Domain.Entities;
using Blog.Domain.Repositories.User;
using Moq;

namespace Tests.Utilities.Repositories;

public class UserReadRepositoryMock {
    private readonly Mock<IUserReadRepository> _mock = new Mock<IUserReadRepository>();

    public IUserReadRepository Build() {
        return _mock.Object;
    }

    public void UserExistsAsync(string email) {
        _mock.Setup(repo => repo.UserWithEmailExistsAsync(email))
            .ReturnsAsync(true);
    }

    public void GetUserByEmailAsync(User user) {
        _mock.Setup(repo => repo.GetUserByEmailAsync(user.Email))
            .ReturnsAsync(user);
    }
}