using Blog.Domain.Repositories.Post;
using Moq;

namespace Tests.Utilities.Repositories;

public class PostWriteRepositoryMock {
    public static IPostWriteRepository Build() {
        var mock = new Mock<IPostWriteRepository>();

        return mock.Object;
    }
}