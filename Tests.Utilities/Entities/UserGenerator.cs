using Blog.Domain.Entities;
using Bogus;

namespace Tests.Utilities.Entities;

public abstract class UserGenerator {
    public static User Generate() {
        var user = new Faker<User>()
            .RuleFor(u => u.Id, Guid.NewGuid)
            .RuleFor(u => u.Username, faker => faker.Name.FirstName())
            .RuleFor(u => u.Email, faker => faker.Internet.Email())
            .RuleFor(u => u.Password, faker => faker.Internet.Password());

        return user;
    }
}