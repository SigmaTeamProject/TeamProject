using Application.Models;
using AutoMapper;
using Data;

namespace Application.Mapping;

public class OrderItemMapper : Profile
{
    public OrderItemMapper()
    {
        CreateMap<OrderItem, BuyProductModel>();
    }
}