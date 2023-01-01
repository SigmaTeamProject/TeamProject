using Application.Exceptions;
using Application.Mapping;
using Application.Models;
using AutoMapper;
using DAL.Repositry;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Product.Update;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductModel>
{
    private readonly IRepository<Data.Product> _repository;
    private readonly IMapper _mapper;
    public UpdateProductCommandHandler(IRepository<Data.Product> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ProductModel> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.Query()
            .Include(product => product.Characteristics)
            .FirstOrDefaultAsync(product => product.Id == request.Product.Id);
        if (product == null)
        {
            throw new NotFoundException(nameof(product), request.Product.Id);
        }

        if (request.Product!.Price > 0)
        {
            throw new ArgumentException();
        }
        product.Price = request.Product.Price ?? product.Price;
        if (!string.IsNullOrEmpty(request.Product.Name))
        {
            product.Name = request.Product.Name;
        }
        await _repository.SaveChangesAsync();
        return _mapper.Map<ProductModel>(product);
    }
}