using Application.Models;
using Data;

namespace Application.Services.Interfaces;

public interface IPaymentService
{
    IList<PaymentConfigModel> GetAllPaymentMethods();
    bool Pay(PaymentConfig paymentConfig);
}