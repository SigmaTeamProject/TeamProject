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
builder.Services.AddContext();
builder.Services.AddAuth(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwagger();
builder.Services.AddServices();
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
