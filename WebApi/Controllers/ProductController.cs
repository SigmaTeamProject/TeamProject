using Application.Dtos;
using Application.Models;
using Application.Queries.Product.GetAllProducts;
using Application.Queries.Product.GetProductById;
using Application.Queries.Product.Update;
using AutoMapper;
using Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public ProductController(IMapper mapper,IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetProductById(int id)
    {
        var command = new GetProductByIdQuery
        {
            Id = id
        };
        return Ok(_mediator.Send(command));
    }

    [HttpGet]
    public async Task<ActionResult> GetAllProducts()
    {
        var command = new GetAllProductQuery
        {

        };
        return Ok(_mediator.Send(command));
    }
    [HttpPost]
    public async Task<ActionResult> Update(UpdateProdcuctInStoregeCommandHandler product)
    {
        var command = new CommandQuery
        {
            Product = product
        };
        return Ok(_mediator.Send(command));
    }

}