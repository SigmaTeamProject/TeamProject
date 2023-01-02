using Application.Commands.Order.Checkout;
using Application.Commands.Order.OrderOrder;
using Application.Dtos;
using Application.Models;
using Application.Queries.Order.GetAllOrders;
using Application.Queries.Order.GetOrder;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : BaseController
{
    [HttpGet("history")]
    public async Task<ActionResult<IEnumerable<OrderPreviewModel>>> GetAll()
    {
        var command = new GetAllOrdersQuery
        {
            CustomerId = UserId
        };
        return Ok(await Mediator.Send(command));
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<OrderModel>> GetById (int id)
    {
        var command = new GetOrderQuery
        {
            Id = id
        };
        return Ok(await Mediator.Send(command));
    }

    [HttpPost]
    public async Task<ActionResult<CheckModel>> OrderOder(PaymentConfigDto? paymentConfigDto)
    {
        var command = new OrderOrderCommand
        {
            CustomerId = UserId,
            PaymentConfigDto = paymentConfigDto
        };
        return Ok(await Mediator.Send(command));
    }
    [HttpPut("payment")]
    public async Task<IActionResult> AddPaymentConfig([FromBody] PaymentConfigDto paymentConfig)
    {
        return Ok();
    }
    [HttpGet]
    public async Task<ActionResult<OrderModel>> Checkout()
    {
        var command = new CheckoutCommand
        {
            CustomerId = UserId,
        };
        return Ok(await Mediator.Send(command));
    }
}