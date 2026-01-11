using Application.Interfaces.CurrentUser;
using Application.Interfaces.JwtService;
using Application.Interfaces.RegisterService;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWorks;
using Application.Users.CredentialChecker;
using Domain.Entities;
using Infrastructure.Currentuser;
using Infrastructure.Data;
using Infrastructure.Filters;
using Infrastructure.Repositories;
using Infrastructure.Repositories.UnitsOfWork;
using Infrastructure.security.CredentialChecker;
using Infrastructure.Services;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
      
        var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
        //
        //Mapper
        services.AddEntityMappers();



        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET")!)),
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        //register filters
        
        services.AddControllers(options =>
        {
            options.Filters.Add<FluentValidationFilter>();
        });

        //Services
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IDeliveryRepository, DeliveryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IAffiliateBalanceRepository, AffiliateBalanceRepository>();
        services.AddScoped<IAuditLogRepository, AuditLogRepository>();
        services.AddScoped<ICallLogRepository, CallLogRepository>();
        services.AddScoped<ICustomizedOrderRepository, CustomizedOrderRepository>();
        services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductImageRepository, ProductImageRepository>();
       
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<IWithdrawalRepository, WithdrawalRepository>();
        services.AddScoped<IFileStorageService, FileStorageService>();
        services.AddScoped<IDriverRepository, DriverRepository>();
        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddHttpContextAccessor();
        services.AddScoped<IAffiliateRepository, AffiliateRepository>();
        services.AddScoped<IAssisstantRepository, AssisstantRepository>();

        services.AddScoped<ICurrentUser, CurrentUser>();

        services.AddScoped<ICredentialChecker, CredentialChecker>();
        services.AddScoped<IRegistrationService, RegistrationService>();


        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        //UoW
        services.AddScoped<IOrderUnitOfWork, OrderUnitOfWork>();
        //token service

        services.AddScoped<IJwtService,TokenService>();

        return services;
    }
}