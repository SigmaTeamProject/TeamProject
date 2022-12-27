using Application.Dtos;
using Application.Models;
using AutoMapper;
using Data;
using MediatR;
using Persistence.Repository;

namespace Application.Queries.Product.Update;

public class UpdateCommandHandler : IRequestHandler<CommandQuery, UpdateProdcuctInStoregeCommandHandler>
{
    private readonly IRepository<UpdateProdcuctInStoregeCommandHandler> _repository;
    private readonly IMapper _mapper;
    public UpdateCommandHandler(IRepository<UpdateProdcuctInStoregeCommandHandler> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<UpdateProdcuctInStoregeCommandHandler> Handle(CommandQuery request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<UpdateProdcuctInStoregeCommandHandler>(request.Product);
        await _repository.UpdateAsync(product);
        return request.Product;
    }
}