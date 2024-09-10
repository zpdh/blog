using Blog.Application.User.Login;
using Blog.Domain.Communication.Requests.User;
using Blog.Exceptions.ExceptionMessages;
using Blog.Exceptions.Exceptions;
using FluentAssertions;
using Tests.Utilities.Entities;
using Tests.Utilities.Repositories;
using Tests.Utilities.Services;

namespace Tests.Application.UseCases.User;

public class LoginUserUseCaseTests {
    [Fact]
    public async Task Success() {
        var user = UserGenerator.Generate();
        var request = new LoginUserRequest(user.Email, user.Password);
        var useCase = CreateUseCase(user);

        var result = await useCase.ExecuteAsync(request);

        result.Username.Should().Be(user.Username);
        result.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task InvalidPasswordError() {
        var user = UserGenerator.Generate();
        var request = new LoginUserRequest(user.Email, "look at me!");
        var useCase = CreateUseCase(user);

        var result = async () => await useCase.ExecuteAsync(request);

        var exception = await result.Should().ThrowAsync<BlogValidationException>();
        exception.Where(e => e.GetErrorMessages().Count == 1 &&
                             e.GetErrorMessages().Contains(ErrorMessages.InvalidPasswordOrEmail));
    }

    [Fact]
    public async Task UserNotFoundError() {
        var user = UserGenerator.Generate();
        var request = new LoginUserRequest(user.Email, user.Password);
        var useCase = CreateUseCase();

        var result = async () => await useCase.ExecuteAsync(request);

        var exception = await result.Should().ThrowAsync<BlogValidationException>();
        exception.Where(e => e.GetErrorMessages().Count == 1 &&
                             e.GetErrorMessages().Contains(ErrorMessages.InvalidPasswordOrEmail));
    }

    private static LoginUserUseCase CreateUseCase(Blog.Domain.Entities.User? user = null) {
        var readRepo = new UserReadRepositoryMock();
        var hasher = PasswordHasherBuilder.Build();
        var tokenGenerator = TokenGeneratorBuilder.Build();

        // If a user is provided, it will simulate the return of a user from the database.
        if (user != null) {
            user.Password = hasher.HashPassword(user.Password);
            readRepo.GetUserByEmailAsync(user);
        }

        return new LoginUserUseCase(
            readRepo.Build(),
            hasher,
            tokenGenerator
        );
    }
}