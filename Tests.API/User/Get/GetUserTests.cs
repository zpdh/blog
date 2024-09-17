using System.Net;
using FluentAssertions;
using Tests.Utilities.Services;

namespace Tests.API.User.Get;

public class GetUserTests : BlogClassFixture {
    private readonly HttpMethod _method = HttpMethod.Get;
    private const string Endpoint = "api/user/get";
    private readonly Blog.Domain.Entities.User _user;

    public GetUserTests(BlogWebApplicationFactory factory)
        : base(factory) {
        _user = factory.User;
    }

    [Fact]
    public async Task Success() {
        var tokenGenerator = TokenGeneratorBuilder.Build();
        var token = tokenGenerator.Generate(_user.Id);

        var response = await SendRequestAsync(_method, Endpoint, token: token);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseContent = await ParseResponseAsync(response);

        responseContent.GetProperty("username").GetString().Should().Be(_user.Username);
        responseContent.GetProperty("email").GetString().Should().Be(_user.Email);
    }

    [Fact]
    public async Task InvalidTokenError() {
        var tokenGenerator = TokenGeneratorBuilder.Build();
        var token = tokenGenerator.Generate(Guid.NewGuid());

        var response = await SendRequestAsync(_method, Endpoint, token: token);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}