using Blog.Application.Mappers;
using Blog.Domain.Communication.Requests.ClassRequests;
using Blog.Domain.Communication.Responses.ClassResponses;
using Blog.Domain.Repositories.UOW;
using Blog.Domain.Repositories.User;
using Blog.Exceptions.ExceptionMessages;
using Blog.Exceptions.Exceptions;
using FluentValidation.Results;

namespace Blog.Application.User.Register;

public interface IRegisterUserUseCase {
    public Task<RegisterUserResponse> ExecuteAsync(RegisterUserRequest request);
}

public class RegisterUserUseCase(
    IUserReadRepository readRepository,
    IUserWriteRepository writeRepository,
    IUnitOfWork unitOfWork
) : IRegisterUserUseCase {

    public async Task<RegisterUserResponse> ExecuteAsync(RegisterUserRequest request) {
        var user = request.MapToUser();

        await ValidateAsync(user);

        await writeRepository.AddUserAsync(user);
        await unitOfWork.CommitAsync();

        return user.MapToResponse();
    }

    private async Task ValidateAsync(Domain.Entities.User user) {
        var validator = new RegisterUserValidator();
        var result = await validator.ValidateAsync(user);

        var exists = await readRepository.UserExistsAsync(user.Email);

        if (exists) {
            result.Errors.Add(new ValidationFailure(string.Empty, ErrorMessages.EmailExists));
        }

        if (!result.IsValid) {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ValidationException(errorMessages);
        }
    }
}