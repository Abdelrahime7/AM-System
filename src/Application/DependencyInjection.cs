using Application.Customers.Features.Commands;
using Application.Customers.Features.Queries;
using Application.Interfaces.CustomerInterfaces;
using Application.Interfaces.DeliveryInterfaces;
using Application.Interfaces.ProductInterfaces;
using Application.Interfaces.UserInterfaces;
using Application.Products.Features.Commands;
using Application.Products.Features.Queries;
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
        //Users Services
        services.AddScoped<IUserCommands, UserCommands>();
        services.AddScoped<IUserQueries, UsersQueries>();
        
        //Customer Services
        services.AddScoped<ICustomerCommands, CustomerCommands>();
        services.AddScoped<ICustomerQueries, CustomerQueries>();
        // Delivery Services
        services.AddScoped<IDeliveryIntegrationQueries, DeliveryIntegrationQueries>();
        services.AddScoped<IDeliveryIntegrationCommands,DeliveryIntgrationCommands>();
        // Product Services
        services.AddScoped<IProductCommands, ProductCommands>();
        services.AddScoped<IProductQueries, ProductQueries>();
        
        
        // validators
        services.AddValidatorsFromAssemblyContaining<CreatUserRequestValidator>();

        return services;


    }
}