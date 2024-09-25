using System.Net;
using Blog.Domain.Communication.Requests.User;
using Blog.Exceptions.ExceptionMessages;
using FluentAssertions;
using Tests.Utilities.Services;

namespace Tests.API.User.ChangeName;

public class ChangeUserNameTests : BlogClassFixture {
    private const string Endpoint = "api/user/change/username";
    private readonly HttpMethod _method = HttpMethod.Put;

    private readonly Blog.Domain.Entities.User _user;

    public ChangeUserNameTests(BlogWebApplicationFactory factory) : base(factory) {
        _user = factory.User;
    }

    [Fact]
    public async Task Success() {
        var newName = new ChangeUsernameUserRequest("new_name");
        var tokenGenerator = TokenGeneratorBuilder.Build();
        var token = tokenGenerator.Generate(_user.Id);

        var response = await SendRequestAsync(_method, Endpoint, newName, token);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task EmptyNameError() {
        var newName = new ChangeUsernameUserRequest(string.Empty);
        var tokenGenerator = TokenGeneratorBuilder.Build();
        var token = tokenGenerator.Generate(_user.Id);

        var response = await SendRequestAsync(_method, Endpoint, newName, token);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var error = (await ParseResponseAsync(response)).GetProperty("errorMessages").EnumerateArray();

        error.Should().ContainSingle()
            .And.Contain(e => e.GetString() == ErrorMessages.EmptyUsername);
    }

    [Fact]
    public async Task InvalidTokenError() {
        var newName = new ChangeUsernameUserRequest("new_name");
        var tokenGenerator = TokenGeneratorBuilder.Build();
        var token = tokenGenerator.Generate(Guid.NewGuid());

        var response = await SendRequestAsync(_method, Endpoint, newName, token);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var error = (await ParseResponseAsync(response)).GetProperty("errorMessages").EnumerateArray();

        error.Should().ContainSingle()
            .And.Contain(e => e.GetString() == ExceptionMessages.NoPermissionsException);
    }

    [Fact]
    public async Task NoTokenError() {
        var newName = new ChangeUsernameUserRequest("new_name");

        var response = await SendRequestAsync(_method, Endpoint, newName);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var error = (await ParseResponseAsync(response)).GetProperty("errorMessages").EnumerateArray();

        error.Should().ContainSingle()
            .And.Contain(e => e.GetString() == ExceptionMessages.NoTokenException);
    }
}