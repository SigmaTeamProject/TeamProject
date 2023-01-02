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
using Application.Commands.StoregeCommands.AddProduct;
using Application.Commands.StoregeCommands.UpdateProduct;
using Application.Queries.Storage.GetAllProducts;
using Application.Queries.Storage.GetProductById;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public StorageController(IMediator mediator,IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StorageItemModel>>> GetAllProducts()
        {
            var command = new GetAllStorageItemsQuery();
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<StorageItemModel>> GetProductById(int id)
        {
            var command = new GetStorageItemQuery
            {
                Id = id
            };
            return Ok(await _mediator.Send(command));
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(StorageItemDto newStorageItem)
        {
            var command = _mapper.Map<AddProductInStorageCommand>(newStorageItem);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateStorageItemDto storageItemToUpdate)
        {
            var command = _mapper.Map<UpdateProductInStorageCommand>(storageItemToUpdate);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
