using Blog.Domain.Entities;
using Blog.Domain.Repositories.Post;
using Blog.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories;

public class PostRepository(
    DataContext context
) : IPostReadRepository, IPostWriteRepository {
    public async Task<bool> PostWithTitleExistsAsync(Guid userId, string title) {
        return await context.Posts
            .AnyAsync(post => post.PostOwner.Id == userId && post.Title == title);
    }

    public bool PostWithCodeExists(string code) {
        return context.Posts
            .Any(post => post.Code == code);
    }

    public async Task AddPostAsync(Post post) {
        await context.Posts.AddAsync(post);
    }
}