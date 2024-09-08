using Blog.Application.Mappers;
using Blog.Application.Services.Code;
using Blog.Domain.Communication.Requests;
using Blog.Domain.Communication.Requests.Post;
using Blog.Domain.Communication.Responses.Post;
using Blog.Domain.Repositories.Post;
using Blog.Domain.Repositories.UOW;
using Blog.Domain.Services;
using Blog.Exceptions.ExceptionMessages;
using Blog.Exceptions.Exceptions;
using FluentValidation.Results;

namespace Blog.Application.Post;

public interface ICreatePostUseCase {
    public Task<CreatePostResponse> ExecuteAsync(CreatePostRequest request);
}

public class CreatePostUseCase(
    ICodeGenerationService codeGenerationService,
    IUserTokenService userTokenService,
    IPostReadRepository readRepository,
    IPostWriteRepository writeRepository,
    IUnitOfWork unitOfWork
) : ICreatePostUseCase {
    public async Task<CreatePostResponse> ExecuteAsync(CreatePostRequest request) {
        var user = await userTokenService.GetUserFromTokenAsync();

        await ValidateRecipeAsync(request, user);

        var code = codeGenerationService.GenerateCode();
        var post = request.MapToPost(code, user);

        await writeRepository.AddPostAsync(post);
        await unitOfWork.CommitAsync();

        var response = post.MapToResponse();

        return response;
    }

    private async Task ValidateRecipeAsync(
        CreatePostRequest request,
        Domain.Entities.User user
    ) {
        var validator = new CreatePostValidator();

        var result = await validator.ValidateAsync(request);

        if (await readRepository.PostWithTitleExistsAsync(user.Id, request.Title)) {
            result.Errors.Add(new ValidationFailure(string.Empty, ErrorMessages.PostWithTitleAlreadyExists));
        }

        if (!result.IsValid) {
            throw new BlogValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
        }
    }
}