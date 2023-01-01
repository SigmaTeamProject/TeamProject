using Application.Dtos;
using Application.Models;
using MediatR;

namespace Application.Commands.Order.Checkout;

public class CheckoutCommand : IRequest<OrderModel>
{
    public int CustomerId { get; set; }
    public PaymentConfigDto? PaymentConfigDto { get; set; }
}