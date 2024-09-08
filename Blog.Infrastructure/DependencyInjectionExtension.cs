using Blog.Domain.Repositories.Post;
using Blog.Domain.Repositories.UOW;
using Blog.Domain.Repositories.User;
using Blog.Domain.Security.Hashing;
using Blog.Domain.Security.Tokens;
using Blog.Domain.Services;
using Blog.Infrastructure.DataAccess;
using Blog.Infrastructure.Repositories;
using Blog.Infrastructure.Security.Hashing;
using Blog.Infrastructure.Security.Tokens;
using Blog.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastructure;

public static class DependencyInjectionExtension {
    public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration) {
        services.AddDataAccess(configuration);
        services.AddRepositories();
        services.AddHashing();
        services.AddTokenHandler(configuration);
        services.AddServices();
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

        services.AddScoped<IPostReadRepository, PostRepository>();
        services.AddScoped<IPostWriteRepository, PostRepository>();
    }

    private static void AddHashing(this IServiceCollection services) {
        services.AddScoped<IPasswordHasher, BCryptHasher>();
    }

    private static void AddTokenHandler(this IServiceCollection services, IConfiguration configuration) {
        var expirationInMinutes = configuration.GetValue<ushort>("Jwt:ExpirationInMinutes");
        var signingKey = configuration["Jwt:SigningKey"];

        var tokenHandler = new JwtTokenHandler(expirationInMinutes, signingKey!);

        services.AddScoped<ITokenGenerator>(_ => tokenHandler);
        services.AddScoped<ITokenValidator>(_ => tokenHandler);
    }

    private static void AddServices(this IServiceCollection services) {
        services.AddScoped<IUserTokenService, UserTokenService>();
    }
}