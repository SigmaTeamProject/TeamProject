using Application.Models;
using MediatR;

public class DeleteProductFromCartCommand : IRequest<CartModel>
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
}

