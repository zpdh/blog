using Blog.Application.User.Login;
using Blog.Application.User.Register;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Application;

public static class DependencyInjectionExtension {
    public static void AddApplicationLayer(this IServiceCollection services) {
        services.AddUseCases();
    }

    private static void AddUseCases(this IServiceCollection services) {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<ILoginUserUseCase, LoginUserUseCase>();
    }
}