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
        IPay pay = new CardPay();
        if (paymentConfig.Type == GetAllPaymentMethods()[0].Type)
        {
            pay = new CardPay();
        }
        if (paymentConfig.Type == GetAllPaymentMethods()[1].Type)
        {
            pay = new PayPalPay();
           
        }
        return pay.Pay(paymentConfig);
    }
}