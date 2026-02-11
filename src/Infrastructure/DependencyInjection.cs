using Application.Interfaces.CurrentUser;
using Application.Interfaces.JwtService;
using Application.Interfaces.RegisterService;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWorks;
using Application.Users.CredentialChecker;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Currentuser;
using Infrastructure.Data;
using Infrastructure.Filters;
using Infrastructure.Repositories;
using Infrastructure.Repositories.UnitsOfWork;
using Infrastructure.security.CredentialChecker;
using Infrastructure.Services;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
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



        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

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
                ValidAudience = Environment.GetEnvironmentVariable("JwtSettings__Audience"),
                ValidIssuer = Environment.GetEnvironmentVariable("JwtSettings__Issuer"),
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JwtSettings__Secret")!)),
                ClockSkew = TimeSpan.Zero
            };
        });
              
        services.AddAuthorization(options => 

        {
        options.AddPolicy("SuperAdminOnly", policy =>
            policy.RequireRole(UserRole.SuperAdmin.ToString()));
            options.AddPolicy("ApprovedAdminOrsuperAdmin", policy =>
                policy.Requirements.Add(new ApprovedRoleRequirement(UserRole.Admin,true)));


            options.AddPolicy("ApprovedAssisstantOnly", policy =>
                policy.Requirements.Add(new ApprovedRoleRequirement(UserRole.Assistant)));
            options.AddPolicy("ApprovedAssisstantOrSuperAdmin", policy =>
               policy.Requirements.Add(new ApprovedRoleRequirement(UserRole.Assistant,true)));
            options.AddPolicy("ApprovedAssisstantOrSuperAdminOrAdmin", policy =>
              policy.Requirements.Add(new ApprovedRoleRequirement(UserRole.Assistant, true,true)));


            options.AddPolicy("ApprovedAffiliateOrSuperAdmin", policy =>
                policy.Requirements.Add(new ApprovedRoleRequirement(UserRole.Affiliate,true)));
            options.AddPolicy("ApprovedAffiliateOnly", policy =>
              policy.Requirements.Add(new ApprovedRoleRequirement(UserRole.Affiliate, true)));
            options.AddPolicy("ApprovedAffiliateOrSuperAdminOrAdmin", policy =>
             policy.Requirements.Add(new ApprovedRoleRequirement(UserRole.Assistant, true, true)));


            options.AddPolicy("ApprovedDriverOrSuperAdmin", policy =>
               policy.Requirements.Add(new ApprovedRoleRequirement(UserRole.Driver,true)));
            options.AddPolicy("ApprovedDriverOrSuperAdminOrAdmin", policy =>
               policy.Requirements.Add(new ApprovedRoleRequirement(UserRole.Driver, true,true)));
            options.AddPolicy("ApprovedDriverOnly", policy =>
                policy.Requirements.Add(new ApprovedRoleRequirement(UserRole.Driver)));
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

        //auth
        services.AddScoped<IAuthorizationHandler, ApprovedRoleHandler>();


        return services;
    }
}