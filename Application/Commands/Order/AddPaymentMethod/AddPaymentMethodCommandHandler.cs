using Application.Models;
using Application.Services.Interfaces;
using DAL.Repositry;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Order.AddPaymentMethod;

public class AddPaymentMethodCommandHandler : IRequestHandler<AddPaymentMethodCommand, PaymentConfigModel>
{
    private readonly IRepository<PaymentConfig> _repository;
    private readonly IRepository<Customer> _customerRepository;
    private readonly IPaymentService _paymentService;
    public AddPaymentMethodCommandHandler(IRepository<PaymentConfig> repository, IPaymentService paymentService, IRepository<Customer> customerRepository)
    {
        _repository = repository;
        _paymentService = paymentService;
        _customerRepository = customerRepository;
    }

    public async Task<PaymentConfigModel> Handle(AddPaymentMethodCommand request, CancellationToken cancellationToken)
    {
        if (_paymentService.GetAllPaymentMethods()
                .FirstOrDefault(model => model.Type == request.PaymentConfigDto.Type) == null)
        {
            throw new ArgumentException("Invalid argument");
        }
        var method = new PaymentConfig
        {
            Type = request.PaymentConfigDto.Type,
            CustomerId = request.UserId
        };
        var customer = await _customerRepository.Query()
            .Include(customer => customer.PaymentConfig)
            .FirstOrDefaultAsync(customer => customer.Id == request.UserId);
        customer!.PaymentConfig = method;
        await _customerRepository.SaveChangesAsync();
        return new PaymentConfigModel
        {
            Type = method.Type
        };
    }
}