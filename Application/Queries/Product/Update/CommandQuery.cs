using Application.Dtos;
using Application.Models;
using MediatR;

namespace Application.Queries.Product.Update;

public class CommandQuery : IRequest<ProductDto>
{
    public ProductDto Product { get; set; }
}