using Blog.Domain.Repositories.User;
using Moq;

namespace Tests.Utilities.Repositories;

public class UserWriteRepositoryMock {
    public static IUserWriteRepository Build() {
        var mock = new Mock<IUserWriteRepository>();

        return mock.Object;
    }
}