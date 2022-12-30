using Application.Dtos;
using Application.Models;
using MediatR;

namespace Application.Commands.Product.Update;

public class UpdateProductCommand : IRequest<ProductModel>
{
    public ProductDto Product { get; set; }
}