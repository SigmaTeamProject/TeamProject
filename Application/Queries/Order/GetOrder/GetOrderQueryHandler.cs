using Application.Exceptions;
using Application.Models;
using AutoMapper;
using DAL.Repositry;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Order.GetOrder;

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, CheckoutModel>
{
    private readonly IRepository<Data.Order> _orderRepository;
    private readonly IMapper _mapper;
    public GetOrderQueryHandler(IRepository<Data.Order> orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<CheckoutModel> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.Query()
            .Include(order => order.Items)
            .ThenInclude(item => item.Product)
            .FirstOrDefaultAsync(order => order.Id == request.Id);
        if (order == null)
        {
            throw new NotFoundException(nameof(Order), request.Id);
        }
        if (order!.CustomerId != request.UserId)
        {
            throw new UnauthorizedAccessException();
        }
        return _mapper.Map<CheckoutModel>(order);
    }
}