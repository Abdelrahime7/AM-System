using Application;
using Application.Admins.Dasboard.DashMetrics.Application.Admin.Dashboard;
using Application.Admins.Dto_s;
using Application.Affiliates.DTO_s;
using Application.Assisstants.Dto_s;
using Application.Drivers.DTO_s;
using Application.RoleRequeste;
using Domain.Enums;
using DotNetEnv;

using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using Serilog;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using WebAPI.Graphql;


var builder = WebApplication.CreateBuilder(args);





// Add Serilog
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

Env.Load();
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Project Title", 
        Version = "v1",
        Description = "Project Description"
    });
    
    // Add JWT authentication to OpenAPI spec
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please Enter 'Bearer' [space] and then your token in the text input below.",
        Name = "Authorization",
        Type = SecuritySchemeType.Http, 
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
    
    // Apply security requirement globally to all operations
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

//Services



builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<GetDashboardMetrics>();


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        options.JsonSerializerOptions.TypeInfoResolverChain.Insert(0, new DefaultJsonTypeInfoResolver
        {
            Modifiers =
            {
                ti =>
                {
                    if (ti.Type == typeof(Role))
                    {
                        ti.PolymorphismOptions = new JsonPolymorphismOptions
                        {
                            TypeDiscriminatorPropertyName = "roleType",
                            IgnoreUnrecognizedTypeDiscriminators = true,
                            UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization
                        };

                        ti.PolymorphismOptions.DerivedTypes.Add(new JsonDerivedType(typeof(CreatAssisstantRequest), nameof(UserRole.Assistant)));
                        ti.PolymorphismOptions.DerivedTypes.Add(new JsonDerivedType(typeof(CreateDriverRequest), nameof(UserRole.Driver)));
                        ti.PolymorphismOptions.DerivedTypes.Add(new JsonDerivedType(typeof(CreateAdminRequest), nameof(UserRole.Admin)));
                        ti.PolymorphismOptions.DerivedTypes.Add(new JsonDerivedType(typeof(CreateAffiliateRequest),nameof(UserRole.Affiliate)));
                    }
                }
            }
        });
    });

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

//Graphql 
builder.Services.AddGraphQLServer().AddAuthorization().
    AddQueryType<Query>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("Project Title")
            .WithTheme(ScalarTheme.Kepler)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
            .WithOpenApiRoutePattern("/swagger/{documentName}/swagger.json"); 
    });
    
    // Auto-migrate database in development
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await context.Database.MigrateAsync();
}


app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.MapControllers();
app.MapGraphQL();


app.Run();