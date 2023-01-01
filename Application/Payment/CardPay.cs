using Application.Dtos;
using Data;

namespace Application.Payment;

public class CardPay : IPay
{
    public bool Pay(PaymentConfig paymentConfig)
    {
        return true;
    }
}