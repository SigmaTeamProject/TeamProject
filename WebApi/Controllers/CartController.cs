using Application.Commands.CartCommands.AddProduct;
using Application.Commands.CartCommands.UpdateProduct;
using Application.Dtos;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CartController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPut("add")]
        [Authorize]
        public async Task<ActionResult> AddProductToCart([FromBody] AddInCartProductDto productToAdd)
        {
            var command = _mapper.Map<AddProductInCartCommand>(productToAdd);
            command.UserId = UserId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCart([FromBody] UpdateProductInCartDto productToUpdate)
        {
            var command = _mapper.Map<UpdateProductInCartCommand>(productToUpdate);
            command.UserId = UserId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
