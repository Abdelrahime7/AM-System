
using Microsoft.Extensions.DependencyInjection;

using Application.Interfaces.UserInterfaces;
using Application.Users.Features.Commands;
using Application.Users.Features.Queries;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //Users Interfaces
        services.AddScoped<IUserCommands, UserCommands>();
        services.AddScoped<IUserQueries, UsersQueries>();
      
        
        return services;


    }
}