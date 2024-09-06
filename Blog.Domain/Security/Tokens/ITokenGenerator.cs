namespace Blog.Domain.Security.Tokens;

public interface ITokenGenerator {
    public string Generate(Guid userId);
}
