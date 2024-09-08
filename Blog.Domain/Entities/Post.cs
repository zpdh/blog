namespace Blog.Domain.Entities;

public class Post : Entity {
    public User PostOwner { get; set; } = default!;
    public string Code { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string TextContent { get; set; } = string.Empty;
}