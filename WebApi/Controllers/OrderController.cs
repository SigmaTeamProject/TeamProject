using Application.Commands.Order.Checkout;
using Application.Commands.Order.OrderOrder;
using Application.Dtos;
using Application.Queries.Order.GetAllOrders;
using Application.Queries.Order.GetOrder;
using Data;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : BaseController
{
    [HttpGet("history")]
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
    public async Task<ActionResult> GetById (int id)
    {
        var command = new GetOrderQuery
        {
            Id = id
        };
        var res = Mediator.Send(command);
        return Ok(res);
    }

    [HttpPost]
    public async Task<ActionResult> OrderOder(PaymentConfigDto? paymentConfigDto)
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
    public async Task<ActionResult> AddPaymentConfig([FromBody] PaymentConfigDto paymentConfig)
    {
        return Ok();
    }
    [HttpGet]
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