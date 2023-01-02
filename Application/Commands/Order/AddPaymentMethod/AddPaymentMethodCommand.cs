using Application.Dtos;
using Application.Models;
using MediatR;

namespace Application.Commands.Order.AddPaymentMethod;

public class AddPaymentMethodCommand : IRequest<PaymentConfigModel>
{
    public int UserId { get; set; }
    public PaymentConfigDto PaymentConfigDto { get; set; }
}