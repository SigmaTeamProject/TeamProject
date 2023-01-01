using Application.Exceptions;
using Application.Models;
using AutoMapper;
using DAL.Repositry;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Product.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery,ProductModel>
{
    private readonly IRepository<Data.Product> _repository;
    private readonly IMapper _mapper;
    public GetProductByIdQueryHandler(IRepository<Data.Product> repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ProductModel> Handle(GetProductByIdQuery request,CancellationToken cancellationToken)
    {
        var product = await _repository.Query()
            .Include(product => product.Characteristics)
            .FirstOrDefaultAsync(product => product.Id == request.Id);
        if (product == null)
        {
            throw new NotFoundException(nameof(product),request.Id);
        }
        return _mapper.Map<ProductModel>(product);
    }
}