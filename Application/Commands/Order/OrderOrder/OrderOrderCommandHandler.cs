using Application.Commands.CartCommands.ClearCart;
using Application.Commands.Order.Checkout;
using Application.Dtos;
using Application.Models;
using Application.Services.Interfaces;
using AutoMapper;
using DAL.Repositry;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Order.OrderOrder;

public class OrderOrderCommandHandler : IRequestHandler<OrderOrderCommand, CheckModel>
{
    private readonly IPaymentService _paymentService;
    private readonly IRepository<Data.Order> _orderDepository;
    private readonly IRepository<Customer> _customerRepository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public OrderOrderCommandHandler(IPaymentService paymentService, IRepository<PaymentConfig> repository, IMapper mapper, IRepository<Customer> customerRepository, IMediator mediator, IRepository<Data.Order> orderDepository)
    {
        _paymentService = paymentService;
        _mapper = mapper;
        _customerRepository = customerRepository;
        _mediator = mediator;
        _orderDepository = orderDepository;
    }

    public async Task<CheckModel> Handle(OrderOrderCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.Query()
            .Include(customer => customer.PaymentConfig)
            .FirstOrDefaultAsync(customer => customer.Id == request.CustomerId, cancellationToken: cancellationToken);
        var paymentConfig = request.PaymentConfigDto == null ? 
            customer!.PaymentConfig 
            : _mapper.Map<PaymentConfig>(request.PaymentConfigDto);
        if (paymentConfig == null)
        {
            throw new InvalidOperationException();
        }
        var isSuccess = _paymentService.Pay(paymentConfig);
        if (!isSuccess)
        {
            throw new InvalidOperationException();
        }

        var clearCartCommand = new ClearCartCommand
        {
            UserId = request.CustomerId
        };
        var checkoutCommand = new CheckoutCommand
        {
            CustomerId = request.CustomerId,
            PaymentConfigDto = _mapper.Map<PaymentConfigDto>(paymentConfig)
        };
        var orderModel = await _mediator.Send(checkoutCommand, cancellationToken);
        var checkModel = new CheckModel
        {
            IsSuccess = isSuccess,
            TotalPrice = orderModel.TotalAmount,
            ProductModels = orderModel.Products.ToList(),
            PaymentMethod = paymentConfig.Type
        };
        var order = _mapper.Map<Data.Order>(orderModel);
        order.CustomerId = request.CustomerId;
        order.Check = _mapper.Map<Check>(checkModel);
        order.Check.OrderId = order.Id;
        order.Items = _mapper.ProjectTo<CartItem>(orderModel.Products.AsQueryable()).ToList();
        order.OrderDate = DateTime.Now;
        order.TotalAmount = orderModel.TotalAmount;
        await _orderDepository.AddAsync(order);
        await _orderDepository.SaveChangesAsync();
        await _mediator.Send(clearCartCommand, cancellationToken);
        return checkModel;
    }
}