using Application.Models;
using AutoMapper;
using DAL.Repositry;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.StoregeCommands.UpdateProduct
{
    public class UpdateProductInStorageCommandHandler : IRequestHandler<UpdateProductInStorageCommand, StorageItemModel>
    {
        private readonly IRepository<StorageItem> _repository;
        private readonly IMapper _mapper;

        public UpdateProductInStorageCommandHandler(IRepository<StorageItem> repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<StorageItemModel> Handle(UpdateProductInStorageCommand request,CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentException();

            var storageItem = await _repository.FirstOrDefaultAsync(item => item.ProductId == request.ProductId);

            if (storageItem == null) throw new ArgumentException();
            if (request.Amount < 0) throw new ArgumentException();

            storageItem.Amount = request.Amount;
            await _repository.UpdateAsync(storageItem);
            await _repository.SaveChangesAsync();

            var item = await _repository
                .Query().Include(i => i.Product)
                .ThenInclude(product => product.Characteristics)
                .FirstOrDefaultAsync(item => item.ProductId == request.ProductId);

            return _mapper.Map<StorageItemModel>(item);
        }
    }
}
