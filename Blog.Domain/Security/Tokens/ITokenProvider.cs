namespace Blog.Domain.Security.Tokens;

public interface ITokenProvider {
    public string GetValue();
}