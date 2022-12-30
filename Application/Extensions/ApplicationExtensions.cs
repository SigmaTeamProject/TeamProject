using Application.Dtos;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        //services.AddTransient<IRequestHandler<UpdateProductCommand, ProductDto>, UpdateProductCommandHandler>();
        return services;
    }
}