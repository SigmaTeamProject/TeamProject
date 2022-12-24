using Application.Queries.Product.GetProductById;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public ProductController(IMapper mapper, IMediator mediator)
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
}