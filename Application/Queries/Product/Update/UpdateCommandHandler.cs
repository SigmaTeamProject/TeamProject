using Application.Dtos;
using Application.Models;
using AutoMapper;
using DAL.Repositry;
using Data;
using MediatR;
using Persistence.Repository;

namespace Application.Queries.Product.Update;

public class UpdateCommandHandler : IRequestHandler<CommandQuery,ProductDto>
{
    private readonly IRepository<ProductDto> _repository;
    private readonly IMapper _mapper;
    public UpdateCommandHandler(IRepository<ProductDto> repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ProductDto> Handle(CommandQuery request,CancellationToken cancellationToken)
    {
        var product = _mapper.Map<ProductDto>(request.Product);
        await _repository.UpdateAsync(product);
        return request.Product;
    }
}