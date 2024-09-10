using Blog.Domain.Communication.Requests.Post;
using Blog.Domain.Entities;
using Blog.Domain.Repositories.Post;
using Moq;

namespace Tests.Utilities.Repositories;

public class PostReadRepositoryMock {
    private readonly Mock<IPostReadRepository> _mock = new();

    public IPostReadRepository Build() {
        return _mock.Object;
    }

    public void PostWithTitleExistsAsync(User user, CreatePostRequest post) {
        _mock.Setup(obj => obj.PostWithTitleExistsAsync(user.Id, post.Title)).ReturnsAsync(true);
    }
}