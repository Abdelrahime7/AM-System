using Application.AffiliatesBalance.Features.Commands;
using Application.AffiliatesBalance.Features.Queries;
using Application.AuditsLog.Features.Commands;
using Application.AuditsLog.Features.Queries;
using Application.CallsLog.Features.Commands;
using Application.CallsLog.Features.Queries;
using Application.Customers.Features.Commands;
using Application.Customers.Features.Queries;
using Application.CustomizedOrders.Features.Commands;
using Application.CustomizedOrders.Features.Queries;
using Application.Interfaces.AffiliateBalanceInterfaces;
using Application.Interfaces.AuditLogInterfaces;
using Application.Interfaces.CallLogInterfaces;
using Application.Interfaces.CustomerInterfaces;
using Application.Interfaces.CustomizedOrderInterfaces;
using Application.Interfaces.DeliveryInterfaces;
using Application.Interfaces.OrderDetailInterfaces;
using Application.Interfaces.OrderInterfaces;
using Application.Interfaces.ProductImagesInterfaces;
using Application.Interfaces.ProductInterfaces;
using Application.Interfaces.RoleInterfaces;
using Application.Interfaces.TokenInterfaces;
using Application.Interfaces.UserInterfaces;
using Application.Interfaces.WithdrawalInterfaces;
using Application.OrderDetails.Features.Commands;
using Application.OrderDetails.Features.Queries;
using Application.Orders.Features.Commands;
using Application.Orders.Features.Queries;
using Application.ProductImages.Features.Commands;
using Application.ProductImages.Features.Queries;
using Application.Products.Features.Commands;
using Application.Products.Features.Queries;
using Application.Roles.Features.Commands;
using Application.Roles.Features.Queries;
using Application.Tokens.Features.Commands;
using Application.Tokens.Features.Queries;
using Application.Users.Features.Commands;
using Application.Users.Features.Queries;
using Application.Users.validation;
using Application.Withdrawals.Features.Commands;
using Application.Withdrawals.Features.Queries;
using Domain.Entities;
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
        // Affiliate Services
        services.AddScoped<IAffiliateBalanceCommands, AffiliateBalanceCommands>();
        services.AddScoped<IAffiliateBalanceQueries, AffiliateBalanceQueries>();
        // AuditLog Services
        services.AddScoped<IAuditLogCommands, AuditLogCommands>();
        services.AddScoped<IAuditLogQueries,AuditLogQueries>();
        // CallLog Services
        services.AddScoped<ICallLogCommands, CallLogCommands>();
        services.AddScoped<ICallLogQueries, CallLogQueries>();
        // CustomizedOrder Services
        services.AddScoped<ICustomizedOrderCommands, CustomizedOrderCommands>();
        services.AddScoped<ICustomizedOrderQueries, CustomizedOrderQueries>();
        // CallLog Services
        services.AddScoped<ICallLogCommands, CallLogCommands>();
        services.AddScoped<ICallLogQueries, CallLogQueries>();
        // Order Services
        services.AddScoped<IOrderCommands, OrderCommands>();
        services.AddScoped<IOrderQueries, OrderQueries>();
        // OrderDetail Services
        services.AddScoped<IOrderDetailCommands, OrderDetailCommands>();
        services.AddScoped<IOrderDetailQueries, OrderDetailQueries>();
        // ProductImage Services
        services.AddScoped<IProductImageCommands, ProductImageCommands>();
        services.AddScoped<IProductImageQueries, ProductImageQueries>();
        // Role Services
        services.AddScoped<IRoleCommands, RoleCommands>();
        services.AddScoped<IRoleQueries, RoleQueries>();
        // Token Services
        services.AddScoped<ITokenCommands, TokenCommands>();
        services.AddScoped<ITokenQueries, TokenQueries>();
        // Token Services
        services.AddScoped<IWithdrawalCommands, WithdrawalCommands>();
        services.AddScoped<IWithdrawalQueries, WithdrawalQueries>();





        // validators
        services.AddValidatorsFromAssemblyContaining<CreatUserRequestValidator>();

        return services;


    }
}