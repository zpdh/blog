namespace Blog.Domain.Repositories.Post;

public interface IPostReadRepository {
    public Task<bool> PostWithTitleExistsAsync(Guid userId,string title);
    public bool PostWithCodeExists(string code);
}