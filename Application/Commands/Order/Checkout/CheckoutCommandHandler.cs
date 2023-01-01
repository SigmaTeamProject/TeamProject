
using Application.Models;
using Application.Services.Interfaces;
using AutoMapper;
using DAL.Repositry;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Order.Checkout;

public class CheckoutCommandHandler : IRequestHandler<CheckoutCommand, OrderModel>
{
    private readonly IRepository<Cart> _cartRepository;
    private readonly IPaymentService _paymentService;
    private readonly IMapper _mapper;
    public CheckoutCommandHandler(IRepository<Cart> cartRepository, IMapper mapper, IPaymentService paymentService)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
        _paymentService = paymentService;
    }

    public async Task<OrderModel> Handle(CheckoutCommand request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.Query()
            .Include(cart => cart.Items)!
            .ThenInclude(item => item.Product)
            .FirstOrDefaultAsync(cart => cart.CustomerId == request.CustomerId);
        if (cart?.Items == null || !cart.Items.Any())
        {
            throw new InvalidOperationException();
        }

        var orderModel = new OrderModel
        {
            PossiblePaymentMethods = _paymentService.GetAllPaymentMethods(),
            Products = _mapper.ProjectTo<BuyProductModel>(cart.Items.AsQueryable()).ToList()
        };
        orderModel.TotalAmount = orderModel.Products.Sum(model => model.Quantity * model.TotalPrice);
        return orderModel;
    }
}