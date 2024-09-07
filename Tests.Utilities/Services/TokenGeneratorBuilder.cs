using Blog.Domain.Security.Tokens;
using Blog.Infrastructure.Security.Tokens;
using Moq;

namespace Tests.Utilities.Services;

public class TokenGeneratorBuilder {
    public static ITokenGenerator Build() {
        return new JwtTokenHandler(30, "6haGd1yG1w7VMObn3gyDPZ4tR8SnAym5");
    }
}