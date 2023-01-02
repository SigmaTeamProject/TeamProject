using Application.Models;
using MediatR;

namespace Application.Commands.CartCommands.ClearCart
{
    public class ClearCartCommand : IRequest<CartModel>
    {
        public int UserId { get; set; }
    }
}
