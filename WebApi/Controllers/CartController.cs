using Application.Commands.CartCommands.AddProduct;
using Application.Commands.CartCommands.ClearCart;
using Application.Commands.CartCommands.UpdateProduct;
using Application.Dtos;
using Application.Models;
using Application.Queries.Cart;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : BaseController
    {
        private readonly IMapper _mapper;

        public CartController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<CartModel>> GetCart()
        {
            var command = new GetCartQuery
            {
                CustomerId = UserId
            };
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("add")]
        [Authorize]
        public async Task<ActionResult<CartModel>> AddProductToCart([FromBody] AddInCartProductDto productToAdd)
        {
            var command = _mapper.Map<AddProductInCartCommand>(productToAdd);
            command.UserId = UserId;
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("update")]
        [Authorize]
        public async Task<ActionResult<CartModel>> UpdateCart([FromBody] UpdateProductInCartDto productToUpdate)
        {
            var command = _mapper.Map<UpdateProductInCartCommand>(productToUpdate);
            command.UserId = UserId;
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("delete")]
        [Authorize]
        public async Task<ActionResult<CartModel>> DeleteProductFromCart([FromBody] DeleteProductFromCartDto productToDelete)
        {
            var command = _mapper.Map<DeleteProductFromCartCommand>(productToDelete);
            command.UserId = UserId;
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("clear")]
        [Authorize]
        public async Task<ActionResult<CartModel>> ClearCart()
        {
            var command = new ClearCartCommand() { UserId = UserId };
            return Ok(await Mediator.Send(command));
        }
    }
}
