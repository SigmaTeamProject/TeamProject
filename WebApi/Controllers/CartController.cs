﻿using Application.Commands.CartCommands.AddProduct;
using Application.Commands.CartCommands.ClearCart;
using Application.Commands.CartCommands.UpdateProduct;
using Application.Dtos;
using Application.Queries.Cart;
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

        [HttpGet]
        public async Task<ActionResult> GetCart()
        {
            var command = new GetCartQuery
            {
                CustomerId = UserId
            };
            var result = await Mediator.Send(command);
            return Ok(result);
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

        [HttpPut("update")]
        [Authorize]
        public async Task<ActionResult> UpdateCart([FromBody] UpdateProductInCartDto productToUpdate)
        {
            var command = _mapper.Map<UpdateProductInCartCommand>(productToUpdate);
            command.UserId = UserId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("delete")]
        [Authorize]
        public async Task<ActionResult> DeleteProductFromCart([FromBody] DeleteProductFromCartDto productToDelete)
        {
            var command = _mapper.Map<DeleteProductFromCartCommand>(productToDelete);
            command.UserId = UserId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("clear")]
        [Authorize]
        public async Task<ActionResult> ClearCart()
        {
            var command = new ClearCartCommand() { UserId = UserId };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
