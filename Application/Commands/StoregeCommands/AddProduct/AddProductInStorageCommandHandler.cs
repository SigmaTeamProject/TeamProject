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
        private readonly IRepository<StorageItem> _repository;

        public AddProductInStorageCommandHandler(IMapper mapper,IRepository<StorageItem> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<StorageItemModel> Handle(AddProductInStorageCommand request,CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentException();

            var storageItem = await _repository.FirstOrDefaultAsync(item => item.ProductId == request.ProductId);

            if (storageItem == null)
            {
                var itemToAdd = _mapper.Map<StorageItem>(request);
                await _repository.AddAsync(itemToAdd);
            }
            else
            {
                if (request.Amount < 0) throw new ArgumentException();

                storageItem.Amount = request.Amount;
                await _repository.UpdateAsync(storageItem);
            }

            await _repository.SaveChangesAsync();

            var item = await _repository
                .Query().Include(i => i.Product)
                .FirstOrDefaultAsync(item => item.ProductId == request.ProductId);

            return _mapper.Map<StorageItemModel>(item);
        }
    }
}
