using Application.Dtos;
using Application.Models;
using MediatR;

namespace Application.Commands.Order.OrderOrder;

public class OrderOrderCommand : IRequest<CheckModel>
{
    public int CustomerId { get; set; }
    public PaymentConfigDto? PaymentConfigDto { get; set; }
}