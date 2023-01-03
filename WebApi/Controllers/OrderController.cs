using System.Diagnostics.CodeAnalysis;
using Application.Commands.Order.AddPaymentMethod;
using Application.Commands.Order.Checkout;
using Application.Commands.Order.OrderOrder;
using Application.Dtos;
using Application.Queries.Order.GetAllOrders;
using Application.Queries.Order.GetOrder;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : BaseController
{
    [HttpGet("history")]
    [Authorize]
    public async Task<ActionResult> GetAll()
    {
        var command = new GetAllOrdersQuery
        {
            CustomerId = UserId
        };
        var res = await Mediator.Send(command);
        return Ok(res);
    }
    
    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<ActionResult> GetById (int id)
    {
        var command = new GetOrderQuery
        {
            Id = id,
            UserId = UserId
        };
        var res = await Mediator.Send(command);
        return Ok(res);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> OrderOder([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)]PaymentConfigDto? paymentConfigDto)
    {
        var command = new OrderOrderCommand
        {
            CustomerId = UserId,
            PaymentConfigDto = paymentConfigDto
        };
        var res = await Mediator.Send(command);
        return Ok(res);
    }
    [HttpPut("payment")]
    [Authorize]
    public async Task<ActionResult> AddPaymentConfig([FromBody] PaymentConfigDto paymentConfig)
    {
        var command = new AddPaymentMethodCommand
        {
            UserId = UserId,
            PaymentConfigDto = paymentConfig
        };
        var res = await Mediator.Send(command);
        return Ok(res);
    }
    [HttpGet]
    [Authorize]
    public async Task<ActionResult> Checkout()
    {
        var command = new CheckoutCommand
        {
            CustomerId = UserId,
        };
        var res = await Mediator.Send(command);
        return Ok(res);
    }
}