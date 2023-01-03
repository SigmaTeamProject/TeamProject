using Application.Models;
using MediatR;

namespace Application.Commands.Product.UpdateProductCharacteristic;

public class UpdateProductCharacteristicCommand : IRequest<ProductCharacteristicModel>
{
    public int? ProductId { get; set; }
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Value { get; set; }
}