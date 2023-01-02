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
        var products = await _cartRepository.Query()
            .Include(cart => cart.Items)!
            .ThenInclude(item => item.Product)
            .FirstOrDefaultAsync(cart => cart.CustomerId == request.CustomerId, cancellationToken);
        
        return null;
    }
}