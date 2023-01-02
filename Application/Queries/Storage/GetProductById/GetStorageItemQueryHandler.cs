using Application.Models;
using AutoMapper;
using DAL.Repositry;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Storage.GetProductById
{
    public class GetStorageItemQueryHandler : IRequestHandler<GetStorageItemQuery, StorageItemModel>
    {
        private readonly IRepository<StorageItem> _repository;
        private readonly IMapper _mapper;
        public GetStorageItemQueryHandler(IRepository<StorageItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<StorageItemModel> Handle(GetStorageItemQuery request, CancellationToken cancellationToken)
        {
            var item = await _repository.Query()
                .Include(i => i.Product)
                .ThenInclude(p => p.Characteristics)
                .FirstOrDefaultAsync(i => i.ProductId == request.Id);
            return _mapper.Map<StorageItemModel>(item);
        }
    }
}
