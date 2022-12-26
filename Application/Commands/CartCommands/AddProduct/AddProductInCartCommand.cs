using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CartCommands.AddProduct
{
    public class AddProductInCartCommand: IRequest<CartModel>
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
