using Application.Commands.Product.Update;
using Application.Commands.Product.UpdateProductCharacteristic;
using Application.Dtos;
using Application.Models;
using Application.Queries.Product.GetAllProducts;
using Application.Queries.Product.GetProductById;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : BaseController
{
    private readonly IMapper _mapper;
    public ProductController(IMapper mapper)
    {
        _mapper = mapper;
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductModel>> GetProductById(int id)
    {
        var command = new GetProductByIdQuery
        {
            Id = id
        };
        return Ok(await Mediator.Send(command));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductPreviewModel>>> GetAllProducts()
    {
        var command = new GetAllProductQuery();
        return Ok(await Mediator.Send(command));
    }
    [HttpPut]
    public async Task<ActionResult<ProductModel>> Update(ProductDto product)
    {
        var command = new UpdateProductCommand
        {
            Product = product
        };
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("characteristic")]
    public async Task<ActionResult<ProductCharacteristicModel>> UpdateProductCharacteristics
        (ProductCharacteristicDto productCharacteristicDto)
    {
        var command = _mapper.Map<UpdateProductCharacteristicCommand>(productCharacteristicDto);
        return Ok(await Mediator.Send(command));
    }
}