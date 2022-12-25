using Application.Models;
using MediatR;

namespace Application.Queries.Product.GetAllProducts;

public class GetAllProductQuery : IRequest<List<ProductModel>>
{

}