using Application.Commands.CartCommands.ClearCart;
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
    private readonly IRepository<Cart> _cartRepository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public OrderOrderCommandHandler(IPaymentService paymentService, IRepository<PaymentConfig> repository, IMapper mapper, IRepository<Customer> customerRepository, IMediator mediator, IRepository<Data.Order> orderDepository, IRepository<Cart> cartRepository)
    {
        _paymentService = paymentService;
        _mapper = mapper;
        _customerRepository = customerRepository;
        _mediator = mediator;
        _orderDepository = orderDepository;
        _cartRepository = cartRepository;
    }

    public async Task<CheckModel> Handle(OrderOrderCommand request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.Query()
            .Include(cart => cart.Items)!
            .ThenInclude(item => item.Product)
            .FirstOrDefaultAsync(cart => cart.CustomerId == request.CustomerId, cancellationToken);
        
        if (cart!.Items is null)
        {
            throw new InvalidOperationException();
        }

        var (paymentConfig, isSuccess) = await Pay(request);
        var order = new Data.Order
        {
            CustomerId = request.CustomerId,
            Items = _mapper.Map<ICollection<OrderItem>>(cart.Items),
            OrderDate = DateTime.Now,
            TotalPrice = cart.Items.Sum(item => item.Amount * item.Product!.Price)
        };
        await _orderDepository.AddAsync(order);
        await _orderDepository.SaveChangesAsync();
        var checkModel = new CheckModel
        {
            TotalAmount = order.TotalPrice,
            IsSuccess = isSuccess,
            PaymentMethod = paymentConfig.Type,
            ProductModels = _mapper.ProjectTo<BuyProductModel>(order.Items.AsQueryable())
        };
        var clearCartCommand = new ClearCartCommand
        {
            UserId = request.CustomerId
        };
        //await _mediator.Send(clearCartCommand);
        return checkModel;
    }

    private async Task<(PaymentConfig, bool)> Pay(OrderOrderCommand request)
    {
        var customer = await _customerRepository.Query()
            .Include(customer => customer.PaymentConfig)
            .FirstOrDefaultAsync(customer => customer.Id == request.CustomerId);
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

        return (paymentConfig, isSuccess);
    }
}