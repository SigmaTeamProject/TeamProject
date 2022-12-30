using Application.Dtos;
using Application.Queries.Product.Update;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        //services.AddTransient<IRequestHandler<CommandQuery, ProductDto>, UpdateCommandHandler>();
        return services;
    }
}