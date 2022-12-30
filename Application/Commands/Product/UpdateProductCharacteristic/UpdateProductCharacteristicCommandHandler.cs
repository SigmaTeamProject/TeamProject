using Application.Models;
using AutoMapper;
using DAL.Repositry;
using Data;
using MediatR;

namespace Application.Commands.Product.UpdateProductCharacteristic;

public class UpdateProductCharacteristicCommandHandler : IRequestHandler<UpdateProductCharacteristicCommand, ProductCharacteristicModel>
{
    private readonly IMapper _mapper;
    private readonly IRepository<ProductCharacteristic> _repository;

    public UpdateProductCharacteristicCommandHandler(IMapper mapper, IRepository<ProductCharacteristic> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ProductCharacteristicModel> Handle(UpdateProductCharacteristicCommand request, CancellationToken cancellationToken)
    {
        var characteristic = await _repository.GetByIdAsync(request.Id);
        characteristic.Name = request.Name ?? characteristic.Name;
        characteristic.Value = request.Value ?? characteristic.Value;
        await _repository.SaveChangesAsync();
        return _mapper.Map<ProductCharacteristicModel>(characteristic);
    }
}