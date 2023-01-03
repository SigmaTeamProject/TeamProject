using Application.Dtos;
using Data;

namespace Application.Payment;

public interface IPay
{
    public bool Pay(PaymentConfig paymentConfig);
}