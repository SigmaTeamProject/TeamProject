using Application.Dtos;
using Application.Models;
using AutoMapper;
using Data;

namespace Application.Mapping;

public class OrderMapper : Profile
{
    public OrderMapper()
    {
        CreateMap<Order, OrderPreviewModel>();
        CreateMap<Check, CheckModel>().ReverseMap();
        CreateMap<PaymentConfig, PaymentConfigDto>().ReverseMap();
        CreateMap<OrderModel, Order>();
    }
}