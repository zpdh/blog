using Blog.API.Filters;
using Blog.API.Tokens;
using Blog.Application;
using Blog.Domain.Security.Tokens;
using Blog.Infrastructure;
using Microsoft.OpenApi.Models;

namespace Blog.API;

public class Program {
    public static async Task Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddMvc(options => options.Filters.Add<ExceptionFilter>());
        builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

        #region Swagger

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options => {
            const string securityScheme = "Bearer";
            options.AddSecurityDefinition(securityScheme, new OpenApiSecurityScheme {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = securityScheme
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                new OpenApiSecurityScheme {
                    Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                        Id = securityScheme
                    },
                    Scheme = "oauth2",
                    Name = securityScheme,
                    In = ParameterLocation.Header
                },
                new List<string>()
                }
            });
        });

        #endregion

        #region Layer Services

        builder.Services.AddScoped<ITokenProvider, TokenProvider>();

        builder.Services.AddApplicationLayer();
        builder.Services.AddInfrastructureLayer(builder.Configuration);

        #endregion

        var app = builder.Build();

// Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        await app.RunAsync();
    }
}