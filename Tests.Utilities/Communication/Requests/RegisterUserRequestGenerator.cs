using Blog.Domain.Communication.Requests;
using Blog.Domain.Communication.Requests.User;
using Bogus;

namespace Tests.Utilities.Communication.Requests;

public static class RegisterUserRequestGenerator {
    public static RegisterUserRequest Generate(int passwordLength = 8) {
        var request = new Faker<RegisterUserRequest>()
            .RuleFor(u => u.Username, faker => faker.Name.FirstName())
            .RuleFor(u => u.Email, faker => faker.Internet.Email())
            .RuleFor(u => u.Password, faker => faker.Internet.Password(passwordLength));

        return request;
    }
}