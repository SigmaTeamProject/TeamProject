using Application.Dtos;
using Application.Models;
using AutoMapper;
using Data;

namespace Application.Mapping;

public class OrderMapper : Profile
{
    public OrderMapper()
    {
        CreateMap<Order, OrderPreviewModel>()
            .ForMember(model => model.TotalAmount,
                opt => opt.MapFrom(order => order.TotalPrice));
        CreateMap<Order, CheckModel>();
        CreateMap<Order, OrderModel>();
        CreateMap<PaymentConfig, PaymentConfigDto>().ReverseMap();
        CreateMap<CheckoutModel, Order>();
    }
}