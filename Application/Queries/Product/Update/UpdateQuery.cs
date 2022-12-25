using Application.Models;
using MediatR;

namespace Application.Queries.Product.Update;

public class UpdateQuery : IRequest<ProductModel>
{
    public ProductModel Product { get; set; }
}