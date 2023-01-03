using Application.Models;
using MediatR;

namespace Application.Commands.CartCommands.UpdateProduct
{
    public class UpdateProductInCartCommand : IRequest<CartModel>
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
