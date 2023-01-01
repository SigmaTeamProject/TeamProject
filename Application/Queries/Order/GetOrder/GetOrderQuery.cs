using Application.Models;
using MediatR;

namespace Application.Queries.Order.GetOrder;

public class GetOrderQuery : IRequest<OrderModel>
{
    public int Id { get; set; }
}