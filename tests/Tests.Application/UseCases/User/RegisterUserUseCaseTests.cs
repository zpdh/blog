using Blog.Application.User.Register;
using Blog.Domain.Communication.Requests.User;
using Blog.Exceptions.ExceptionMessages;
using Blog.Exceptions.Exceptions;
using FluentAssertions;
using Tests.Utilities.Communication.Requests;
using Tests.Utilities.Repositories;
using Tests.Utilities.Services;

namespace Tests.Application.UseCases.User;

public class RegisterUserUseCaseTests {
    [Fact]
    public async Task Success() {
        var request = RegisterUserRequestGenerator.Generate();
        var useCase = CreateUseCase();

        var result = await useCase.ExecuteAsync(request);

        result.Username.Should().Be(request.Username);
        result.Email.Should().Be(request.Email);
        result.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task InvalidUsernameError() {
        var request = RegisterUserRequestGenerator.Generate();
        request.Username = string.Empty;
        var useCase = CreateUseCase();

        var result = async () => await useCase.ExecuteAsync(request);

        var thrownException = await result.Should().ThrowAsync<BlogValidationException>();
        thrownException.Where(e => e.GetErrorMessages().Count == 1 &&
                                   e.GetErrorMessages().Contains(ErrorMessages.EmptyUsername));
    }

    [Fact]
    public async Task EmptyEmailError() {
        var request = RegisterUserRequestGenerator.Generate();
        request.Email = string.Empty;
        var useCase = CreateUseCase();

        var result = async () => await useCase.ExecuteAsync(request);

        var thrownException = await result.Should().ThrowAsync<BlogValidationException>();
        thrownException.Where(e => e.GetErrorMessages().Count == 1 &&
                                   e.GetErrorMessages().Contains(ErrorMessages.EmptyEmail));
    }

    [Fact]
    public async Task InvalidEmailError() {
        var request = RegisterUserRequestGenerator.Generate();
        request.Email = "www.google.com";
        var useCase = CreateUseCase();

        var result = async () => await useCase.ExecuteAsync(request);

        var thrownException = await result.Should().ThrowAsync<BlogValidationException>();
        thrownException.Where(e => e.GetErrorMessages().Count == 1 &&
                                   e.GetErrorMessages().Contains(ErrorMessages.InvalidEmail));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public async Task InvalidPasswordError(int passwordLength) {
        var request = RegisterUserRequestGenerator.Generate(passwordLength);
        var useCase = CreateUseCase();

        var result = async () => await useCase.ExecuteAsync(request);

        var thrownException = await result.Should().ThrowAsync<BlogValidationException>();
        thrownException.Where(e => e.GetErrorMessages().Count == 1 &&
                                   e.GetErrorMessages().Contains(ErrorMessages.InvalidPassword));
    }

    [Fact]
    public async Task EmailExistsError() {
        var request = RegisterUserRequestGenerator.Generate();
        var useCase = CreateUseCase(request);

        var result = async () => await useCase.ExecuteAsync(request);

        var thrownException = await result.Should().ThrowAsync<BlogValidationException>();
        thrownException.Where(e => e.GetErrorMessages().Count == 1 &&
                                   e.GetErrorMessages().Contains(ErrorMessages.EmailExists));
    }

    // If the request gets sent to this method, it'll throw a validation exception (since user with email exists)
    private static RegisterUserUseCase CreateUseCase(RegisterUserRequest? request = null) {
        var readRepo = new UserReadRepositoryMock();
        var writeRepo = UserWriteRepositoryMock.Build();
        var unitOfWork = UnitOfWorkMock.Build();
        var passwordHasher = PasswordHasherBuilder.Build();
        var tokenGenerator = TokenGeneratorBuilder.Build();

        if (request is not null) {
            readRepo.UserExistsAsync(request.Email);
        }

        return new RegisterUserUseCase(
            readRepo.Build(),
            writeRepo,
            unitOfWork,
            passwordHasher,
            tokenGenerator
        );
    }
}