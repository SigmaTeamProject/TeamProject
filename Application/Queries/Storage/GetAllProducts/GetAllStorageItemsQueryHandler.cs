using Application.Models;
using AutoMapper;
using DAL.Repositry;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Storage.GetAllProducts
{
    public class GetAllStorageItemsQueryHandler : IRequestHandler<GetAllStorageItemsQuery, IEnumerable<StorageItemModel>>
    {
        private readonly IRepository<StorageItem> _repository;
        private readonly IMapper _mapper;
        public GetAllStorageItemsQueryHandler(IRepository<StorageItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<StorageItemModel>> Handle(GetAllStorageItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await _repository.Query().Include(i => i.Product).ToListAsync();
            return _mapper.Map<IEnumerable<StorageItemModel>>(items);
        }
    }
}
