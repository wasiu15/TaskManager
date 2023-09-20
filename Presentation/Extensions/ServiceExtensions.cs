using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Hangfire;
using Hangfire.MySql;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.Services.TokenManager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shared.HttpServices.Services;
using System.Reflection;
using System.Text;

namespace Presentation.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
              services.AddCors(options =>
              {
                  options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-Pagination"));
              });
        public static void ConfigureDatabaseContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts => opts.UseMySql(configuration.GetConnectionString("sqlConnection"), ServerVersion.AutoDetect(configuration.GetConnectionString("sqlConnection"))));
            //services.AddDbContext<RepositoryContext>(opts => opts.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        
        public static void ConfigureRepositoryManager(this IServiceCollection services) => services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) => services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureTokenManager(this IServiceCollection services) => services.AddScoped<ITokenManager, TokenManager>();

        public static void ConfigureHttpclient(this IServiceCollection services)
        {
            services.AddHttpClient<IHttpService, HttpService>();
            services.AddScoped<IHttpService, HttpService>();
        }
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration) =>
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(t =>
            {
                t.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:key"])),
                    ValidateIssuer = true,
                    ValidIssuer = configuration["jwt:audience"],
                    ValidAudience = configuration["jwt:audience"],
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });

        public static void ConfigureAuthorization(this IServiceCollection services) =>
            services.AddAuthorization(opt =>
            {
                opt.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
            });

        public static void ConfigureHangfire(this IServiceCollection services, IConfiguration _configuration) =>
            services.AddHangfire(configuration => configuration
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseStorage(
                        new MySqlStorage(
                            _configuration.GetConnectionString("sqlConnection"),
                            new MySqlStorageOptions
                            {
                                TablesPrefix = "Hangfire",
                            })
            ));

        public static void ConfigureSwaggerGen(this IServiceCollection services)
        {
            //  To fix the reguler error on this project->properties->Build->Output-> select 'Generate a file containing API documentation
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Task Management API",
                    Description = "An ASP.NET Core Web API for a Task Management System.",

                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Description = @"Put *ONLY* your JWT Bearer token in textbox below!.
                        <br/>
                        Example: 'eyshdhdhdh'",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = JwtBearerDefaults.AuthenticationScheme,
                            Name = JwtBearerDefaults.AuthenticationScheme,
                            In = ParameterLocation.Header
                        },
                            new List<string>()
                        }
                });
            });
        }
    }
}