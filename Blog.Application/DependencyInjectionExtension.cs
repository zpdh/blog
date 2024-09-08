using Blog.Application.Post;
using Blog.Application.Services.Code;
using Blog.Application.User.Login;
using Blog.Application.User.Register;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Application;

public static class DependencyInjectionExtension {
    public static void AddApplicationLayer(this IServiceCollection services) {
        services.AddUseCases();
        services.AddServices();
    }

    private static void AddUseCases(this IServiceCollection services) {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<ILoginUserUseCase, LoginUserUseCase>();

        services.AddScoped<ICreatePostUseCase, CreatePostUseCase>();
    }

    private static void AddServices(this IServiceCollection services) {
        services.AddScoped<ICodeGenerationService, CodeGenerationService>();
    }
}