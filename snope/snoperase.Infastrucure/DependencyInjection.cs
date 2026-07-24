using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using snoperase.Application.Interface;
using snoperase.Application.Repositories;
using snoperase.Application.Security;
using snoperase.Infastrucure.Data;

namespace snoperase.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddInfastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddSingleton<IJwtProvider, JwtProvider>();
        return services;
    }
}