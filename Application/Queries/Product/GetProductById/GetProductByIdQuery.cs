using Application.Models;
using MediatR;

namespace Application.Queries.Product.GetProductById;

public class GetProductByIdQuery : IRequest<ProductModel>
{
    public int Id { get; set; }
}