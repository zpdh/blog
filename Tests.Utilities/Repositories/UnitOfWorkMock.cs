using Blog.Domain.Repositories.UOW;
using Moq;

namespace Tests.Utilities.Repositories;

public class UnitOfWorkMock {
    public static IUnitOfWork Build() {
        var mock = new Mock<IUnitOfWork>();

        return mock.Object;
    }
}