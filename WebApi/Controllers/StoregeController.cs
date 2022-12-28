using AutoMapper;
using Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity.Infrastructure;
using DAL.Context;
using System.Data.Entity;
using Application.Models;
using Application.Dtos;
using Application.Queries.Product.GetProductById;
using Application.Queries.Product.GetAllProducts;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoregeController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public StoregeController(IMediator mediator,IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductPreviewModel>>> GetAllProducts()
        {
            var command = new GetAllProductQuery
            {

            };
            return Ok(_mediator.Send(command));
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ProductModel>> GetProductById(int Id)
        {
            var command = new GetProductByIdQuery
            {
                Id = Id
            };
            return Ok(_mediator.Send(command));
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(ProductModel newProduct)
        {
            var prod = new Product
            {
                Name = newProduct.Name,
                Price = newProduct.Price
            };

            var command = _mapper.Map<Product>(prod);
            var result = await _mediator.Send(command);
            return Ok(result);

        }

        [HttpPut("{Id:int}")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto updateProduct)
        {
            var command = _mapper.Map<ProductDto>(updateProduct);
            var result = await _mediator.Send(command);
            return Ok(result);
        }




    }
}
