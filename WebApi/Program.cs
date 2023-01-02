using System.Reflection;
using Application.Commands.CartCommands.AddProduct;
using Application.Extensions;
using Application.Services.Implementation;
using Application.Services.Interfaces;
using DAL.Context;
using DAL.Repository;
using DAL.Repositry;
using Data;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using NLog;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ILoggerManager,LoggerManager>();
builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

builder.Services.AddAuth(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwagger();
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddDbContext<ApplicationDbContext>();
/*builder.Services.AddDbContext<ApplicationDbContext>(
    optionsBuilder => optionsBuilder
        .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!, b => b.MigrationsAssembly("WebApi"))
        .EnableSensitiveDataLogging()
    );*/
builder.Services.AddDbContext<ApplicationDbContext>(
    optionsBuilder => optionsBuilder
        .UseNpgsql("Host=localhost;Username=aloshaprokopenko5;Password=787898;Database=sigma_db")
        .EnableSensitiveDataLogging()
);
builder.Services.AddMediator();
builder.Services.AddScoped<ITokenManager, TokenManager>();
//builder.Services.AddEntityFrameworkNpgsql();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IRepository<Customer>, Repository<Customer>>();
builder.Services.AddScoped<IRepository<Order>, Repository<Order>>();
builder.Services.AddScoped<IRepository<Product>, Repository<Product>>();
builder.Services.AddScoped<IRepository<Cart>, Repository<Cart>>();
builder.Services.AddScoped<IRepository<StorageItem>, Repository<StorageItem>>();
builder.Services.AddScoped<IRepository<CartItem>, Repository<CartItem>>();
builder.Services.AddScoped<IRepository<ProductCharacteristic>, Repository<ProductCharacteristic>>();
builder.Services.AddScoped<IRepository<PaymentConfig>, Repository<PaymentConfig>>();
builder.Services.AddScoped<ITokenManager, TokenManager>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior",true);
var app = builder.Build();

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),"/nlog.config"));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRequestLocalization(opts =>
{
    opts.AddSupportedCultures("en-US")
        .AddSupportedUICultures("en-US")
        .SetDefaultCulture("en-US");
});

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
using var scope = app.Services.CreateScope();
var provider = scope.ServiceProvider;
var context = provider.GetRequiredService<ApplicationDbContext>();
try
{
    context.Database.EnsureCreated();
}
catch (Exception e)
{
    Console.WriteLine(e);
}
app.Run();
