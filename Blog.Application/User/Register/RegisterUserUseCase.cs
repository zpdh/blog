using Blog.Application.Mappers;
using Blog.Domain.Communication.Requests.ClassRequests;
using Blog.Domain.Communication.Responses.ClassResponses;
using Blog.Exceptions.Exceptions;

namespace Blog.Application.User.Register;

public interface IRegisterUserUseCase {
    public Task<RegisterUserResponse> ExecuteAsync(RegisterUserRequest request);
}

public class RegisterUserUseCase : IRegisterUserUseCase {

    public async Task<RegisterUserResponse> ExecuteAsync(RegisterUserRequest request) {
        var user = request.MapToUser();

        await ValidateAsync(user);

        return user.MapToResponse();
    }

    private async Task ValidateAsync(Domain.Entities.User user) {
        var validator = new RegisterUserValidator();
        var result = await validator.ValidateAsync(user);

        if (!result.IsValid) {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ValidationException(errorMessages);
        }


    }
}