namespace Blog.Domain.Entities;

public abstract class Entity {
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
}