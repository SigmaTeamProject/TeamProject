using Application.Commands.CartCommands.AddProduct;
using Application.Dtos;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CartController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPut]
        public async Task<ActionResult> AddProduct([FromBody] AddInCartProductDto productToAdd)
        {
            var command = _mapper.Map<AddProductCommand>(productToAdd);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct([FromBody] UpdateProductInCartDto productToUpdate)
        {
            var command = _mapper.Map<AddProductCommand>(productToUpdate);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
