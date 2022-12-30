using Application.Commands.Product;
using Application.Commands.Product.Update;
using Application.Commands.Product.UpdateProductCharacteristic;
using Application.Dtos;
using Application.Models;
using Application.Queries.Product.GetAllProducts;
using Application.Queries.Product.GetProductById;
using AutoMapper;
using Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public ProductController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetProductById(int id) //works
    {
        var command = new GetProductByIdQuery
        {
            Id = id
        };
        return Ok(await _mediator.Send(command));
    }

    [HttpGet]
    public async Task<ActionResult> GetAllProducts() //works
    {
        var command = new GetAllProductQuery
        {

        };
        return Ok(await _mediator.Send(command));
    }
    [HttpPost]
    public async Task<ActionResult> Update(ProductDto product) //works
    {
        var command = new UpdateProductCommand
        {
            Product = product
        };
        return Ok(await _mediator.Send(command));
    }

    [HttpPost("characteristic")]
    public async Task<ActionResult> UpdateProductCharacteristics
        (ProductCharacteristicDto productCharacteristicDto) //works
    {
        var command = _mapper.Map<UpdateProductCharacteristicCommand>(productCharacteristicDto);
        return Ok(await _mediator.Send(command));
    }
}