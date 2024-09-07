using Blog.Domain.Repositories.User;
using Moq;

namespace Tests.Utilities.Repositories;

public class UserReadRepositoryMock {
    private readonly Mock<IUserReadRepository> _mock = new Mock<IUserReadRepository>();

    public IUserReadRepository Build() {
        return _mock.Object;
    }

    public void UserExistsAsync(string email) {
        _mock.Setup(repo => repo.UserExistsAsync(email)).ReturnsAsync(true);
    }
}