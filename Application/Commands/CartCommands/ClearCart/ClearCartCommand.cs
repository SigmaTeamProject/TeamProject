using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CartCommands.ClearCart
{
    public class ClearCartCommand : IRequest<CartModel>
    {
        public int UserId { get; set; }
    }
}
