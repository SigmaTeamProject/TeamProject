using Application.Models;
using MediatR;

namespace Application.Queries.Order.GetOrder;

public class GetOrderQuery : IRequest<CheckoutModel>
{
    public int Id { get; set; }
}