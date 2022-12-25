using Application.Models;
using AutoMapper;
using Data;
using MediatR;
using Persistence.Repository;

namespace Application.Queries.Product.Update;

public class UpdateCommandHandler : IRequestHandler<UpdateQuery, ProductModel>
{
    private readonly IRepository<Data.Product> _repository;
    private readonly IMapper _mapper;
    public UpdateCommandHandler(IRepository<Data.Product> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ProductModel> Handle(UpdateQuery request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Data.Product>(request.Product);
        await _repository.UpdateAsync(product);
        return request.Product;
    }
}