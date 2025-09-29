
using Application.Customers.Features.Commands;
using Application.Customers.Features.Queries;
using Application.Interfaces.CustomerInterfaces;
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
        
        //Customer interfaces
        services.AddScoped<ICustomerCommands, CustomerCommands>();
        services.AddScoped<ICustomerQueries, CustomerQueries>();

        // validators
        services.AddValidatorsFromAssemblyContaining<CreatUserRequestValidator>();

        return services;


    }
}