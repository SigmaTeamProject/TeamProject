using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Application.Models;
using Application.Dtos;
using Application.Commands.StoregeCommands.AddProduct;
using Application.Commands.StoregeCommands.UpdateProduct;
using Application.Queries.Storage.GetAllProducts;
using Application.Queries.Storage.GetProductById;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : BaseController
    {
        private readonly IMapper _mapper;

        public StorageController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "AdminModerator")]
        public async Task<ActionResult<IEnumerable<StorageItemModel>>> GetAllProducts()
        {
            var command = new GetAllStorageItemsQuery();
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<StorageItemModel>> GetProductById(int id)
        {
            var command = new GetStorageItemQuery
            {
                Id = id
            };
            return Ok(await Mediator.Send(command));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<StorageItemModel>> AddProduct(StorageItemDto newStorageItem)
        {
            var command = _mapper.Map<AddProductInStorageCommand>(newStorageItem);
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<StorageItemModel>> UpdateProduct(UpdateStorageItemDto storageItemToUpdate)
        {
            var command = _mapper.Map<UpdateProductInStorageCommand>(storageItemToUpdate);
            return Ok(await Mediator.Send(command));
        }
    }
}
