using Application.Models;
using MediatR;

namespace Application.Queries.Cart;

public class GetCartQuery : IRequest<CartModel>
{
    public int CustomerId { get; set; }
}