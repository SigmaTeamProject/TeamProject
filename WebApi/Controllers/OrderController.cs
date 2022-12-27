using Application.Commands.CartCommands.AddProduct;
using Application.Commands.CartCommands.UpdateProduct;
using Application.Commands.OrderCommands.BuyOrder;
using Application.Dtos;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : BaseController
{
	private readonly IMediator _mediator;
	private readonly IMapper _mapper;

	public OrderController(IMediator mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper = mapper;
	}

	[HttpPut]
	public async Task<ActionResult> BuyOrder([FromBody] BuyOrderDto orderDto)
	{
		var command = _mapper.Map<BuyOrderCommand>(orderDto);
		var customerId = UserId;
		command.CustomerId = customerId;
		var result = await _mediator.Send(command);
		return Ok(result);
	}

	[HttpPut]
	public async Task<ActionResult> AddPaymentMethod([FromBody] AddPaymentMethodDto paymentMethodDto)
	{
		var command = _mapper.Map<AddPaymentMethodDto>(paymentMethodDto);
		command.PaymentMethod = "PayPal";
		var result = await _mediator.Send(command);
		return Ok(result);
	}

	[HttpGet]
    public async Task<ActionResult> GetAll()
    {
        return Ok();
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetById (int id)
    {
        return Ok();
    }
}