using Application.Models;
using AutoMapper;
using DAL.Repositry;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.StoregeCommands.AddProduct
{
    public class AddProductInStorageCommandHandler : IRequestHandler<AddProductInStorageCommand, StorageItemModel>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<StorageItem> _storageItemRepository;
        private readonly IRepository<Data.Product> _productRepository;

        public AddProductInStorageCommandHandler(IMapper mapper,
            IRepository<StorageItem> storageItemRepository,
            IRepository<Data.Product> productRepository)
        {
            _mapper = mapper;
            _storageItemRepository = storageItemRepository;
            _productRepository = productRepository;
        }

        public async Task<StorageItemModel> Handle(AddProductInStorageCommand request,CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentException();

            if (_productRepository.Query().Any(product => product.Name == request.Name && product.Price == request.Price))
                throw new ArgumentException("Item with this name already added Id = " + 
                                            _productRepository.FirstOrDefaultAsync(product => product.Name == request.Name).Result!.Id);

            if (request.Amount < 0 || request.Price < 0 || request.Name == "") throw new ArgumentException();

            var productToAdd = new Data.Product
            {
                Name = request.Name,
                Price = request.Price
            };

            await _productRepository.AddAsync(productToAdd);
            await _productRepository.SaveChangesAsync();

            var addedProduct = await _productRepository
                .FirstOrDefaultAsync(product => product.Name == productToAdd.Name && product.Price == productToAdd.Price);

            var itemToAdd = new StorageItem
            {
                ProductId = addedProduct.Id,
                Amount = request.Amount
            };

            await _storageItemRepository.AddAsync(itemToAdd);
            await _storageItemRepository.SaveChangesAsync();

            var itemToReturn = await _storageItemRepository.Query()
                .Include(item => item.Product)
                .ThenInclude(product => product.Characteristics)
                .FirstOrDefaultAsync(item => item.ProductId == addedProduct.Id);

            return _mapper.Map<StorageItemModel>(itemToReturn);
        }
    }
}
