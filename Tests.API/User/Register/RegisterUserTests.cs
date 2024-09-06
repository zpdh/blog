using System.Net;
using System.Text.Json;
using Blog.Domain.Communication.Responses.User;
using Blog.Exceptions.ExceptionMessages;
using FluentAssertions;
using Tests.Utilities.Communication.Requests;

namespace Tests.API.User.Register;

public class RegisterUserTests(BlogWebApplicationFactory factory) : BlogClassFixture(factory) {
    private readonly HttpMethod _method = HttpMethod.Post;
    private const string Endpoint = "api/user/register";

    [Fact]
    public async Task Success() {
        var request = RegisterUserRequestGenerator.Generate();

        var response = await SendRequestAsync(_method, Endpoint, request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var responseContent = await ParseResponseAsync(response);

        responseContent.GetProperty("username").GetString().Should().Be(request.Username);
        responseContent.GetProperty("email").GetString().Should().Be(request.Email);
    }

    [Fact]
    public async Task InvalidUsernameError() {
        var request = RegisterUserRequestGenerator.Generate();
        request.Username = string.Empty;

        var response = await SendRequestAsync(_method, Endpoint, request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var responseContent = await ParseResponseAsync(response);

        var errors = responseContent.GetProperty("errorMessages").EnumerateArray();

        errors.Should().ContainSingle().And.Contain(error => error.GetString()!.Equals(ErrorMessages.EmptyUsername));
    }
}