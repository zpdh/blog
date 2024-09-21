using Blog.Domain.Communication.Requests.User;
using Blog.Domain.Repositories.UOW;
using Blog.Domain.Repositories.User;
using Blog.Domain.Services;
using Blog.Exceptions.ExceptionMessages;
using Blog.Exceptions.Exceptions;
using FluentValidation.Results;

namespace Blog.Application.User.ChangeName;

public interface IChangeUserNameUseCase {
    Task Execute(ChangeUsernameUserRequest request);
}

public class ChangeUserNameUseCase(
    IUserReadRepository readRepository,
    IUserWriteRepository writeRepository,
    IUnitOfWork unitOfWork,
    IUserTokenService userTokenService
) : IChangeUserNameUseCase {

    public async Task Execute(ChangeUsernameUserRequest request) {
        var user = await userTokenService.GetUserFromTokenAsync();

        await ValidateAsync(request, user);

        user.Username = request.NewName;
        writeRepository.UpdateUser(user);

        await unitOfWork.CommitAsync();
    }

    private static async Task ValidateAsync(ChangeUsernameUserRequest request, Domain.Entities.User user) {
        var validator = new ChangeUserNameValidator();

        var result = await validator.ValidateAsync(request);

        if (!result.IsValid) {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new BlogValidationException(errorMessages);
        }
    }
}