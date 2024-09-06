namespace Blog.Domain.Security.Tokens;

public interface ITokenValidator {
    public Guid ValidateAndGetUserId(string token);
}