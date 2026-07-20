using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using snoperase.Application.Interface;
using snoperase.Application.Security;

namespace snoperase.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddInfastructure(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
        return services;
    }
}