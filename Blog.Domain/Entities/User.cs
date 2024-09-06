namespace Blog.Domain.Entities;

public sealed class User : Entity {
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}