using Blog.Domain.Repositories.UOW;
using Blog.Domain.Repositories.User;
using Blog.Domain.Security.Hashing;
using Blog.Infrastructure.DataAccess;
using Blog.Infrastructure.Repositories;
using Blog.Infrastructure.Security.Hashing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastructure;

public static class DependencyInjectionExtension {
    public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration) {
        services.AddDataAccess(configuration);
        services.AddRepositories();
        services.AddHashing();
    }

    private static void AddDataAccess(this IServiceCollection services, IConfiguration configuration) {
        var isTestEnvironment = configuration.GetValue<bool>("IsTestEnvironment");

        if (isTestEnvironment) {
            return;
        }

        var connectionString = configuration["ConnectionStrings:DefaultConnection"];
        var serverVersion = ServerVersion.AutoDetect(connectionString);

        services.AddDbContext<DataContext>(options => options.UseMySql(connectionString, serverVersion));
    }

    private static void AddRepositories(this IServiceCollection services) {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserReadRepository, UserRepository>();
        services.AddScoped<IUserWriteRepository, UserRepository>();
    }

    private static void AddHashing(this IServiceCollection serviceCollection) {
        serviceCollection.AddScoped<IPasswordHasher, BCryptHasher>();
    }
}