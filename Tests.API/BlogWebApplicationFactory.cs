using Blog.API;
using Blog.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.API;

public class BlogWebApplicationFactory : WebApplicationFactory<Program> {
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

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        });
    }
}