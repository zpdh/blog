using Blog.Application.User.Get;
using Blog.Exceptions.ExceptionMessages;
using Blog.Exceptions.Exceptions;
using FluentAssertions;
using Tests.Utilities.Entities;
using Tests.Utilities.Services;

namespace Tests.Application.UseCases.User;

public class GetUserUseCaseTests {
    [Fact]
    public async Task Success() {
        var user = UserGenerator.Generate();
        var useCase = CreateUseCase(user);

        var result = await useCase.Execute();

        result.Username.Should().Be(user.Username);
        result.Email.Should().Be(user.Email);
    }

    [Fact]
    public async Task UserNotFoundError() {
        var useCase = CreateUseCase();

        var result = async () => await useCase.Execute();

        var exception = await result.Should().ThrowAsync<BlogNotFoundException>();
        exception.Where(e => e.GetErrorMessages().Count == 1 &&
                             e.GetErrorMessages().Contains(ErrorMessages.UserNotFound));
    }

    private static GetUserUseCase CreateUseCase(Blog.Domain.Entities.User? user = null) {
        var userTokenService = new UserTokenServiceBuilder();

        if (user is not null) {
            userTokenService.GetUserFromTokenAsync(user);
        }

        return new GetUserUseCase(userTokenService.Build());
    }
}