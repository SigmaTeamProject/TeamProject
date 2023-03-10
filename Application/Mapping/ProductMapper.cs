using Application.Commands.Product.UpdateProductCharacteristic;
using Application.Dtos;
using Application.Models;
using AutoMapper;
using Data;

namespace Application.Mapping;

public class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<CartItem, BuyProductModel>()
            .ForMember(model => model.Amount, 
                opt => opt.MapFrom(item => item.Amount))
            .ForMember(model => model.TotalPrice,
                opt => opt
                    .MapFrom(item => item.Amount * item.Product!.Price))
            .ForMember(model => model.Id, 
                opt => opt.MapFrom(item => item.ProductId))
            .ForMember(model => model.Price, 
                opt => opt.MapFrom(item => item.Product!.Price))
            .ForMember(model => model.TotalPrice, 
                opt => opt.MapFrom(item => item.Product!.Price * item.Amount))
            .ForMember(model => model.Name, 
                opt => opt.MapFrom(item => item.Product!.Name));
        CreateMap<ProductCharacteristicDto, UpdateProductCharacteristicCommand>();
        CreateMap<ProductCharacteristic, ProductCharacteristicModel>().ReverseMap();
        CreateMap<UpdateProductCharacteristicCommand, ProductCharacteristic>();
        CreateMap<Product, ProductPreviewModel>();
        CreateMap<ProductDto, Product>().ReverseMap();
        CreateMap<Product, ProductModel>()
            .ForMember(model => model.Characteristics, 
                opt => opt.MapFrom(product => product.Characteristics));
    }
}