using Blog.Application.Post;
using Blog.Application.Services.Code;
using Blog.Domain.Communication.Requests.Post;
using Blog.Exceptions.ExceptionMessages;
using Blog.Exceptions.Exceptions;
using FluentAssertions;
using Tests.Utilities.Communication.Requests;
using Tests.Utilities.Entities;
using Tests.Utilities.Repositories;
using Tests.Utilities.Services;

namespace Tests.Application.UseCases.Post;

public class CreatePostUseCaseTests {
    [Fact]
    public async Task Success() {
        var user = UserGenerator.Generate();
        var post = CreatePostRequestGenerator.Generate();
        var useCase = CreateUseCase(user);

        var result = await useCase.ExecuteAsync(post);

        result.Code.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task InvalidTitleError() {
        var user = UserGenerator.Generate();
        var post = CreatePostRequestGenerator.Generate();
        post.Title = string.Empty;

        var useCase = CreateUseCase(user);

        var result = async () => await useCase.ExecuteAsync(post);

        var error = await result.Should().ThrowAsync<BlogValidationException>();
        error.Where(e => e.GetErrorMessages().Count == 1 &&
                         e.GetErrorMessages().Contains(ErrorMessages.EmptyTitle));
    }

    [Fact]
    public async Task InvalidTextContentError() {
        var user = UserGenerator.Generate();
        var post = CreatePostRequestGenerator.Generate();
        post.TextContent = string.Empty;

        var useCase = CreateUseCase(user);

        var result = async () => await useCase.ExecuteAsync(post);

        var error = await result.Should().ThrowAsync<BlogValidationException>();
        error.Where(e => e.GetErrorMessages().Count == 1 &&
                         e.GetErrorMessages().Contains(ErrorMessages.EmptyTextContent));
    }

    [Fact]
    public async Task PostExistsError() {
        var user = UserGenerator.Generate();
        var post = CreatePostRequestGenerator.Generate();
        var useCase = CreateUseCase(user, post);

        var result = async () => await useCase.ExecuteAsync(post);

        var error = await result.Should().ThrowAsync<BlogValidationException>();
        error.Where(e => e.GetErrorMessages().Count == 1 &&
                         e.GetErrorMessages().Contains(ErrorMessages.PostWithTitleAlreadyExists));
    }

    private static CreatePostUseCase CreateUseCase(
        Blog.Domain.Entities.User user,
        CreatePostRequest? post = null
    ) {
        var readRepo = new PostReadRepositoryMock();
        var tokenService = new UserTokenServiceBuilder();

        tokenService.GetUserFromTokenAsync(user);

        if (post != null) {
            readRepo.PostWithTitleExistsAsync(user, post);
        }


        var codeGenerator = CodeGenerationServiceBuilder.Build(readRepo.Build());
        var writeRepo = PostWriteRepositoryMock.Build();
        var unitOfWork = UnitOfWorkMock.Build();

        return new CreatePostUseCase(codeGenerator, tokenService.Build(), readRepo.Build(), writeRepo, unitOfWork);
    }
}