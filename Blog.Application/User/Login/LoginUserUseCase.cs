using System.Diagnostics.CodeAnalysis;
using Blog.Application.Mappers;
using Blog.Domain.Communication.Requests.User;
using Blog.Domain.Communication.Responses.User;
using Blog.Domain.Repositories.User;
using Blog.Domain.Security.Hashing;
using Blog.Domain.Security.Tokens;
using Blog.Exceptions.ExceptionMessages;
using Blog.Exceptions.Exceptions;

namespace Blog.Application.User.Login;

public interface ILoginUserUseCase {
    public Task<LoginUserResponse> ExecuteAsync(LoginUserRequest request);
}

public class LoginUserUseCase(
    IUserReadRepository readRepository,
    IPasswordHasher hasher,
    ITokenGenerator tokenGenerator
) : ILoginUserUseCase {
    public async Task<LoginUserResponse> ExecuteAsync(LoginUserRequest request) {
        var userInDb = await readRepository.GetUserByEmailAsync(request.Email);

        if (userInDb is null || !hasher.Verify(request.Password, userInDb.Password)) {
            throw new BlogValidationException([ErrorMessages.InvalidPasswordOrEmail]);
        }

        var token = tokenGenerator.Generate(userInDb.Id);

        return userInDb.MapToLoginResponse(token);
    }
}