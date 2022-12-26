using Application.Models;
using AutoMapper;
using Data;
using MediatR;
using Persistence.Repository;

namespace Application.Queries.Product.GetProductById;

public class GetProductByIdCommandHandler : IRequestHandler<GetProductByIdQuery, ProductModel>
{
    private readonly IRepository<Data.Product> _repository;
    private readonly IMapper _mapper;
    public GetProductByIdCommandHandler(IRepository<Data.Product> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ProductModel> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id);
        return _mapper.Map<ProductModel>(product);
    }
}