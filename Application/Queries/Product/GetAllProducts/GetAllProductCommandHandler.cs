using Application.Models;
using AutoMapper;
using Data;
using MediatR;
using Persistence.Repository;

namespace Application.Queries.Product.GetAllProducts;

public class GetAllProductCommandHandler : IRequestHandler<GetAllProductQuery, IEnumerable<ProductModel>>
{
    private readonly IRepository<Data.Product> _repository;
    private readonly IMapper _mapper;
    public GetAllProductCommandHandler(IRepository<Data.Product> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<ProductModel>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        var products = await _repository.GetAllAsync();
        return products.Select(product => _mapper.Map<ProductModel>(products));
    }
}