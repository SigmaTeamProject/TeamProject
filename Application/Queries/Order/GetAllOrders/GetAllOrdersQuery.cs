using Application.Models;
using MediatR;

namespace Application.Queries.Order.GetAllOrders;

public class GetAllOrdersQuery : IRequest<IEnumerable<OrderPreviewModel>>
{
    public int CustomerId { get; set; }
}