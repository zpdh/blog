namespace Blog.Domain.Repositories.Post;

public interface IPostWriteRepository {
    public Task AddPostAsync(Entities.Post post);
}