using System.Reflection;
using Application.Dtos;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddMediator(this IServiceCollection services, Assembly[] getAssemblies)
    {
        //services.AddTransient<IRequestHandler<UpdateProductCommand, ProductDto>, UpdateProductCommandHandler>();
        return services;
    }
}