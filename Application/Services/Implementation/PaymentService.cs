using Application.Models;
using Application.Payment;
using Application.Services.Interfaces;
using Data;

namespace Application.Services.Implementation;

public class PaymentService : IPaymentService
{
    public IList<PaymentConfigModel> GetAllPaymentMethods()
    {
        var payByCardConfig = new PaymentConfigModel
        {
            Type = "Card"
        };
        var payByPayPalConfig = new PaymentConfigModel
        {
            Type = "PayPal"
        };
        return new List<PaymentConfigModel>() {payByCardConfig, payByPayPalConfig};
    }

    public bool Pay(PaymentConfig paymentConfig)
    {
        IPay pay;
        if (paymentConfig.Type.ToLower() == GetAllPaymentMethods()[0].Type.ToLower())
        {
            pay = new CardPay();
            return pay.Pay(paymentConfig);
        }
        if (paymentConfig.Type.ToLower() == GetAllPaymentMethods()[1].Type.ToLower())
        {
            pay = new PayPalPay();
            return pay.Pay(paymentConfig);
        }

        throw new InvalidOperationException("Incorrect payment method!");
    }
}