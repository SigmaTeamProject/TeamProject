using Application.Exceptions;
using Application.Models;
using AutoMapper;
using DAL.Repositry;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Product.UpdateProductCharacteristic;

public class UpdateProductCharacteristicCommandHandler : IRequestHandler<UpdateProductCharacteristicCommand, ProductCharacteristicModel>
{
    private readonly IMapper _mapper;
    private readonly IRepository<ProductCharacteristic> _repository;
    private readonly IRepository<Data.Product> _productRepository;
    public UpdateProductCharacteristicCommandHandler(IMapper mapper, IRepository<ProductCharacteristic> repository, IRepository<Data.Product> productRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _productRepository = productRepository;
    }

    public async Task<ProductCharacteristicModel> Handle(UpdateProductCharacteristicCommand request, CancellationToken cancellationToken)
    {
        if (request.Id == null)
        {
            var product = await _productRepository.Query()
                .Include(product => product.Characteristics)
                .FirstOrDefaultAsync(product => product.Id == request!.ProductId, cancellationToken: cancellationToken);
            if (product == null)
            {
                throw new NotFoundException(nameof(Data.Product), request.ProductId!);
            }

            var chr = _mapper.Map<ProductCharacteristic>(request);
            product.Characteristics.Add(chr);
            await _productRepository.SaveChangesAsync();
            return _mapper.Map<ProductCharacteristicModel>(chr);
        }
        var characteristic = await _repository.GetByIdAsync(request.Id.Value);
        characteristic.Name = request.Name ?? characteristic.Name;
        characteristic.Value = request.Value ?? characteristic.Value;
        await _repository.SaveChangesAsync();
        return _mapper.Map<ProductCharacteristicModel>(characteristic);
    }
}