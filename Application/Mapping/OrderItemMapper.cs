using Application.Models;
using AutoMapper;
using Data;

namespace Application.Mapping;

public class OrderItemMapper : Profile
{
    public OrderItemMapper()
    {
        CreateMap<OrderItem, CartItem>()
            .ForMember(cart => cart.Amount,
                opt => opt.MapFrom(order => order.Amount))
            .ForMember(cart => cart.Product,
                opt => opt.MapFrom(order => order.Product));
        CreateMap<CartItem, OrderItem>()
            .ForMember(orderItem => orderItem.Amount,
                opt => opt.MapFrom(cartItem => cartItem.Amount))
            .ForMember(orderItem => orderItem.Product,
                opt => opt.MapFrom(cartItem => cartItem.Product))
            .ForMember(orderItem => orderItem.Price, 
                opt => opt.MapFrom(cartItem => cartItem.Product!.Price))
            .ForMember(orderItem => orderItem.ProductId, 
                opt => opt.MapFrom(cartItem => cartItem.ProductId));
        CreateMap<OrderItem, BuyProductModel>()
            .ForMember(model => model.Amount, 
                opt => opt.MapFrom(item => item.Amount))
            .ForMember(model => model.Id, 
                opt => opt.MapFrom(item => item.ProductId))
            .ForMember(model => model.Name, 
                opt => opt.MapFrom(item => item.Product.Name))
            .ForMember(model => model.Price, 
                opt => opt.MapFrom(item => item.Price))
            .ForMember(model => model.TotalPrice, 
                opt => opt.MapFrom(item => item.Price * item.Amount));
    }
}