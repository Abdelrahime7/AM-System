
using Application.Interfaces.UserInterfaces;
using Application.Users.Features.Commands;
using Application.Users.Features.Queries;
using Application.Users.validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //Users Interfaces
        services.AddScoped<IUserCommands, UserCommands>();
        services.AddScoped<IUserQueries, UsersQueries>();


        // validators
        services.AddValidatorsFromAssemblyContaining<CreatUserRequestValidator>();

        return services;


    }
}