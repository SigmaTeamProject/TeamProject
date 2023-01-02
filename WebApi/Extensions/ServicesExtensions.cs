using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.OpenApi.Any;
using DAL.Repositry;
using Data;
using DAL.Repository;
using Application.Services.Interfaces;
using Application.Services.Implementation;
using DAL.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Authentication Login",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JsonWebToken",
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
                options.MapType<DateOnly>(() => new OpenApiSchema
                {
                    Type = "string",
                    Format = "date",
                    Example = new OpenApiString("2022-01-01")
                });
            });
        }

        public static void AddServices(this IServiceCollection services)
        {
            //Mediator
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

            //TokenManager
            services.AddScoped<ITokenManager, TokenManager>();

            //PaymentService
            services.AddScoped<IPaymentService, PaymentService>();

            //AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Repositories
            services.AddScoped<IModeratorService, ModeratorService>();
            services.AddScoped<IRepository<Customer>, Repository<Customer>>();
            services.AddScoped<IRepository<Order>, Repository<Order>>();
            services.AddScoped<IRepository<Product>, Repository<Product>>();
            services.AddScoped<IRepository<Cart>, Repository<Cart>>();
            services.AddScoped<IRepository<StorageItem>, Repository<StorageItem>>();
            services.AddScoped<IRepository<CartItem>, Repository<CartItem>>();
            services.AddScoped<IRepository<ProductCharacteristic>, Repository<ProductCharacteristic>>();
            services.AddScoped<IRepository<PaymentConfig>, Repository<PaymentConfig>>();
        }

        public static void AddContext(this IServiceCollection services)
        {
            //services.AddDbContext<ApplicationDbContext>();
            /*services.AddDbContext<ApplicationDbContext>(
                optionsBuilder => optionsBuilder
                    .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!, b => b.MigrationsAssembly("WebApi"))
                    .EnableSensitiveDataLogging()
                );*/
            services.AddDbContext<ApplicationDbContext>(
                optionsBuilder => optionsBuilder
                    .UseNpgsql("Host=localhost;Username=aloshaprokopenko5;Password=787898;Database=sigma_db")
                    .EnableSensitiveDataLogging()
            );
        }
    }
}
