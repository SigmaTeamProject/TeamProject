using Application.Dtos;

using AutoMapper;
using Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity.Infrastructure;
using DAL.Context;
using System.Data.Entity;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoregeController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public StoregeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts() => await _context.Products.Select(s => new ProductDto { Id = s.Id,Name = s.Name,Price = s.Price,Characteristics = s.Characteristics }).ToListAsync();

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int Id)
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
        public async Task<ActionResult> AddProduct(ProductDto newProduct)
        {
            var product = new Product
            {
                Name = newProduct.Name,
                Price = newProduct.Price,
                Characteristics = newProduct.Characteristics,
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductById),new { Id = product.Id },product);
        }

        [HttpPut("{Id:int}")]
        public async Task<IActionResult> UpdateProduct(int Id,ProductDto productDTO)
        {
            if (Id != productDTO.Id)
            {
                return BadRequest();
            }

            var product = await _context.Products.FindAsync(Id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                productDTO.Name = productDTO.Name;
                product.Characteristics = productDTO.Characteristics;
                productDTO.Price = productDTO.Price;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ProductExists(Id))
            {
                return NotFound();
            }

            return NoContent();
        }

        private bool ProductExists(int Id)
        {
            return _context.Products.Any(p => p.Id == Id);
        }


    }
}
