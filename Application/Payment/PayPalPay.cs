using Application.Dtos;
using Data;

namespace Application.Payment;

public class PayPalPay : IPay
{
    public bool Pay(PaymentConfig paymentConfig)
    {
        return true;
    }
}