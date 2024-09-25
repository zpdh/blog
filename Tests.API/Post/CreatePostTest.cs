using System.Net;
using Blog.Domain.Security.Tokens;
using Blog.Exceptions.ExceptionMessages;
using FluentAssertions;
using Tests.Utilities.Communication.Requests;
using Tests.Utilities.Services;

namespace Tests.API.Post;

public class CreatePostTest : BlogClassFixture {
    private readonly HttpMethod _method = HttpMethod.Post;
    private const string Endpoint = "api/post/create";

    private readonly ITokenGenerator _tokenGenerator;
    private readonly Blog.Domain.Entities.User _user;

    public CreatePostTest(BlogWebApplicationFactory factory) : base(factory) {
        _tokenGenerator = TokenGeneratorBuilder.Build();
        _user = factory.User;
    }

    [Fact]
    public async Task Success() {
        var token = _tokenGenerator.Generate(_user.Id);
        var request = CreatePostRequestGenerator.Generate();

        var response = await SendRequestAsync(_method, Endpoint, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var responseContent = await ParseResponseAsync(response);
        responseContent.GetProperty("code").ToString().Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task NoTitleError() {
        var token = _tokenGenerator.Generate(_user.Id);
        var request = CreatePostRequestGenerator.Generate();
        request.Title = string.Empty;

        var response = await SendRequestAsync(_method, Endpoint, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var responseContent = await ParseResponseAsync(response);
        var errors = responseContent.GetProperty("errorMessages").EnumerateArray();

        errors
            .Should()
            .ContainSingle()
            .And
            .Contain(error => error.GetString()! == ErrorMessages.EmptyTitle);
    }

    [Fact]
    public async Task NoTokenError() {
        var request = CreatePostRequestGenerator.Generate();

        var response = await SendRequestAsync(_method, Endpoint, request);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var responseContent = await ParseResponseAsync(response);

        var errors = responseContent.GetProperty("errorMessages").EnumerateArray();
        errors
            .Should()
            .ContainSingle()
            .And
            .Contain(error => error.GetString() == ExceptionMessages.NoTokenException);
    }

    [Fact]
    public async Task InvalidTokenError() {
        var token = _tokenGenerator.Generate(Guid.NewGuid());
        var request = CreatePostRequestGenerator.Generate();

        var response = await SendRequestAsync(_method, Endpoint, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var responseContent = await ParseResponseAsync(response);

        var errors = responseContent.GetProperty("errorMessages").EnumerateArray();
        errors
            .Should()
            .ContainSingle()
            .And
            .Contain(error => error.GetString() == ExceptionMessages.NoPermissionsException);
    }
}