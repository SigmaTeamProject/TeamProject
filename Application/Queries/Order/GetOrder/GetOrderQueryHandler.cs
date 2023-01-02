using Application.Models;
using MediatR;

namespace Application.Queries.Order.GetOrder;

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, CheckoutModel>
{
    public Task<CheckoutModel> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}