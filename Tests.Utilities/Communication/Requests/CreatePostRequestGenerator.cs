using Blog.Domain.Communication.Requests.Post;
using Bogus;
using Moq;

namespace Tests.Utilities.Communication.Requests;

public class CreatePostRequestGenerator {
    public static CreatePostRequest Generate() {
        return new Faker<CreatePostRequest>()
            .RuleFor(post => post.Title, faker => faker.Lorem.Word())
            .RuleFor(post => post.TextContent, faker => faker.Lorem.Text());
    }
}