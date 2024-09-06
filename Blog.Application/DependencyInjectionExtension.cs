using Blog.Application.User.Register;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Application;

public static class DependencyInjectionExtension {
    public static void AddApplicationLayer(this IServiceCollection services) {
    }

    private static void AddUseCases(this IServiceCollection services) {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
    }
}