using Blog.API;
using Blog.Infrastructure.DataAccess;
using Blog.Infrastructure.Security.Hashing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tests.Utilities.Entities;

namespace Tests.API;

public class BlogWebApplicationFactory : WebApplicationFactory<Program> {
    public Blog.Domain.Entities.User User { get; private set; } = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder) {
        builder.UseEnvironment("Tests").ConfigureServices(services => {
            var descriptor = services
                .SingleOrDefault(desc => desc.ServiceType == typeof(DbContextOptions<DataContext>));

            if (descriptor != null) {
                services.Remove(descriptor);
            }

            services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("BlogTestDb"));

            using var scope = services.BuildServiceProvider().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();

            InitializeDatabase(context);
        });
    }

    private void InitializeDatabase(DataContext context) {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        var userInDb = HashUserPassword();

        context.Users.Add(userInDb);

        context.SaveChanges();
    }

    private Blog.Domain.Entities.User HashUserPassword() {
        var hasher = new BCryptHasher();

        User = UserGenerator.Generate();

        // Need to do this in order for the object to not be copied by reference
        var userInDb = new Blog.Domain.Entities.User {
            Id = User.Id,
            CreationDate = User.CreationDate,
            Email = User.Email,
            Username = User.Username,
            Password = hasher.HashPassword(User.Password)
        };

        return userInDb;
    }
}