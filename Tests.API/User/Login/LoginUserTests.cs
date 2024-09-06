using Blog.Domain.Communication.Requests.User;
using Blog.Exceptions.ExceptionMessages;
using FluentAssertions;

namespace Tests.API.User.Login;

public class LoginUserTests : BlogClassFixture {
    private readonly HttpMethod _method = HttpMethod.Post;
    private const string Endpoint = "api/user/login";

    private readonly Blog.Domain.Entities.User _user;

    public LoginUserTests(BlogWebApplicationFactory factory) : base(factory) {
        _user = factory.User;
    }

    [Fact]
    public async Task Success() {
        var request = new LoginUserRequest(_user.Email, _user.Password);

        var response = await SendRequestAsync(_method, Endpoint, request);

        var responseContent = await ParseResponseAsync(response);

        responseContent.GetProperty("username").GetString().Should().Be(_user.Username);
        responseContent.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task InvalidPasswordError() {
        var request = new LoginUserRequest(_user.Email, "look at me!");

        var response = await SendRequestAsync(_method, Endpoint, request);

        var responseContent = await ParseResponseAsync(response);

        var errors = responseContent.GetProperty("errorMessages").EnumerateArray();

        errors.Should().ContainSingle().And
            .Contain(e => e.GetString()! == ErrorMessages.InvalidPasswordOrEmail);
    }
}