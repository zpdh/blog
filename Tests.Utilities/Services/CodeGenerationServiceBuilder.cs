using Blog.Application.Services.Code;
using Blog.Domain.Repositories.Post;
using Blog.Domain.Repositories.User;
using Tests.Utilities.Repositories;

namespace Tests.Utilities.Services;

public class CodeGenerationServiceBuilder {
    public static CodeGenerationService Build(IPostReadRepository readRepo) {
        return new CodeGenerationService(readRepo);
    }
}