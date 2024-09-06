using Blog.Domain.Repositories.UOW;
using Blog.Domain.Repositories.User;
using Blog.Infrastructure.DataAccess;
using Blog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastructure;

public static class DependencyInjectionExtension {
    public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration) {
        services.AddDataAccess(configuration);
        services.AddRepositories();
    }

    private static void AddDataAccess(this IServiceCollection services, IConfiguration configuration) {
        var connectionString = configuration["ConnectionStrings:DefaultConnection"];
        var serverVersion = ServerVersion.AutoDetect(connectionString);

        services.AddDbContext<DataContext>(options => options.UseMySql(connectionString, serverVersion));
    }

    private static void AddRepositories(this IServiceCollection services) {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserReadRepository, UserRepository>();
        services.AddScoped<IUserWriteRepository, UserRepository>();
    }
}