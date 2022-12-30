using Application.Dtos;
using Application.Models;
using AutoMapper;
using DAL.Repositry;
using Data;
using MediatR;

namespace Application.Queries.Product.Update;

public class UpdateCommandHandler : IRequestHandler<CommandQuery, Data.Product>
{
    private readonly IRepository<Data.Product> _repository;
    private readonly IMapper _mapper;
    public UpdateCommandHandler(IRepository<Data.Product> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Data.Product> Handle(CommandQuery request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Data.Product>(request.Product);
        await _repository.UpdateAsync(product);
        await _repository.SaveChangesAsync();
        return new Data.Product();
    }
}