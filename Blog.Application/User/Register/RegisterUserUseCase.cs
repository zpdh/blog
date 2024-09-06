using Blog.Application.Mappers;
using Blog.Domain.Communication.Requests;
using Blog.Domain.Communication.Requests.User;
using Blog.Domain.Communication.Responses.User;
using Blog.Domain.Repositories.UOW;
using Blog.Domain.Repositories.User;
using Blog.Domain.Security.Hashing;
using Blog.Domain.Security.Tokens;
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
    IUnitOfWork unitOfWork,
    IPasswordHasher hasher,
    ITokenGenerator tokenGenerator
) : IRegisterUserUseCase {
    public async Task<RegisterUserResponse> ExecuteAsync(RegisterUserRequest request) {
        var user = request.MapToUser();

        await ValidateUserAsync(user);

        user.Password = hasher.HashPassword(user.Password);

        await SaveUserAsync(user);

        var token = tokenGenerator.Generate(user.Id);

        return user.MapToRegisterResponse(token);
    }

    private async Task ValidateUserAsync(Domain.Entities.User user) {
        var validator = new RegisterUserValidator();
        var result = await validator.ValidateAsync(user);

        var exists = await readRepository.UserExistsAsync(user.Email);

        if (exists) {
            result.Errors.Add(new ValidationFailure(string.Empty, ErrorMessages.EmailExists));
        }

        if (!result.IsValid) {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new BlogValidationException(errorMessages);
        }
    }

    private async Task SaveUserAsync(Domain.Entities.User user) {
        await writeRepository.AddUserAsync(user);
        await unitOfWork.CommitAsync();
    }
}