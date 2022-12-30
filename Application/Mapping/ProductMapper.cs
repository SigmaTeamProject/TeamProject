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
        CreateMap<ProductCharacteristicDto, UpdateProductCharacteristicCommand>();
        CreateMap<ProductCharacteristic, ProductCharacteristicModel>().ReverseMap();
        CreateMap<Product, ProductPreviewModel>();
        CreateMap<ProductDto, Product>().ReverseMap();
        CreateMap<Product, ProductModel>()
            .ForMember(model => model.Characteristics, 
                opt => opt.MapFrom(product => product.Characteristics));
    }
}