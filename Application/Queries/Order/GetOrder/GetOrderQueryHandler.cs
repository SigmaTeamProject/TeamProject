using Application.Models;
using MediatR;

namespace Application.Queries.Order.GetOrder;

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderModel>
{
    public Task<OrderModel> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}