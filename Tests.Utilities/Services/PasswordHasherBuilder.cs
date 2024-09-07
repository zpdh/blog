using Blog.Domain.Security.Hashing;
using Blog.Infrastructure.Security.Hashing;

namespace Tests.Utilities.Services;

public class PasswordHasherBuilder {
    public static IPasswordHasher Build() {
        return new BCryptHasher();
    }
}