using AutoMapper;
using Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity.Infrastructure;
using DAL.Context;
using System.Data.Entity;
using Application.Models;
using Application.Dtos;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoregeController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public StoregeController(ApplicationDbContext context,IMediator mediator,IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductPreviewModel>>> GetAllProducts() => await _context.Products.Select(s => new ProductPreviewModel { Name = s.Name,Price = s.Price,}).ToListAsync();

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ProductModel>> GetProductById(int Id)
        {
            var product = await _context.Products.FindAsync(Id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(product);
            }


        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(ProductModel newProduct)
        {
            var product = new Product
            {
                Name = newProduct.Name,
                Price = newProduct.Price,
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductById),new { Id = product.Id },product);
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
