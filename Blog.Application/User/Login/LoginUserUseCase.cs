using Blog.Application.Mappers;
using Blog.Domain.Communication.Requests.User;
using Blog.Domain.Repositories.User;
using Blog.Domain.Security.Hashing;
using Blog.Exceptions.ExceptionMessages;
using Blog.Exceptions.Exceptions;

namespace Blog.Application.User.Login;

public interface ILoginUserUseCase {
    public Task Execute(LoginUserRequest request);
}

public class LoginUserUseCase(
    IUserReadRepository readRepository,
    IPasswordHasher hasher
) : ILoginUserUseCase {


    public async Task Execute(LoginUserRequest request) {
        var userInDb = await readRepository.GetUserByEmailAsync(request.Email);

        ValidateRequest(request, userInDb);

        userInDb.MapToLoginResponse();
    }

    private void ValidateRequest(LoginUserRequest request, Domain.Entities.User? user) {
        if (user is null || !hasher.Verify(request.Password, user.Password)) {
            throw new BlogValidationException([ErrorMessages.InvalidPasswordOrEmail]);
        }
    }
}