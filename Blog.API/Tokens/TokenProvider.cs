using Blog.Domain.Security.Tokens;

namespace Blog.API.Tokens;

public class TokenProvider(IHttpContextAccessor accessor) : ITokenProvider {
    public string GetValue() {
        var authorizationHeaders = accessor.HttpContext!.Request.Headers.Authorization.ToString();

        return authorizationHeaders["Bearer ".Length..].Trim();
    }
}