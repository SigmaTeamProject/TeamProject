using Application.Models;
using AutoMapper;
using DAL.Repositry;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Product.GetAllProducts;

public class GetAllProductCommandHandler : IRequestHandler<GetAllProductQuery, IEnumerable<ProductPreviewModel>>
{
    private readonly IRepository<Data.Product> _repository;
    private readonly IMapper _mapper;
    public GetAllProductCommandHandler(IRepository<Data.Product> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<ProductPreviewModel>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        var products = await _repository.Query()
            .Include(product => product.StorageItem)
            .ToListAsync(cancellationToken);
        products = products.Where(product => product.StorageItem!.Amount != 0).ToList();
        return _mapper.ProjectTo<ProductPreviewModel>(products.AsQueryable());
    }
}