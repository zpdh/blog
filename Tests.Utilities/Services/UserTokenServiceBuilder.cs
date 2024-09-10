using Blog.Domain.Entities;
using Blog.Domain.Services;
using Moq;

namespace Tests.Utilities.Services;

public class UserTokenServiceBuilder {
    private readonly Mock<IUserTokenService> _mock = new();

    public IUserTokenService Build() {
        return _mock.Object;
    }

    public void GetUserFromTokenAsync(User user) {
        _mock.Setup(obj => obj.GetUserFromTokenAsync()).ReturnsAsync(user);
    }
}