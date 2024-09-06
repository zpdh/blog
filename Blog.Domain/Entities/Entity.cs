namespace Blog.Domain.Entities;

public abstract class Entity {
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
}