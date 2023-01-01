using Application.Commands.CartCommands.AddProduct;
using Application.Commands.CartCommands.UpdateProduct;
using Application.Dtos;
using Application.Models;
using AutoMapper;
using Data;

namespace Application.Mapping;

public class CartMapper : Profile
{
    public CartMapper()
    {
        CreateMap<AddInCartProductDto, AddProductInCartCommand>().ReverseMap();
        CreateMap<UpdateProductInCartDto, UpdateProductInCartCommand>().ReverseMap();
        CreateMap<DeleteProductFromCartDto, DeleteProductFromCartCommand>().ReverseMap();

        CreateMap<BuyProductModel,CartItem>()
            .ForPath(cart => cart.Product!.Id,
            opt => opt.MapFrom(p => p.Id))
            .ForPath(cart => cart.Product!.Name,
            opt => opt.MapFrom(p => p.Name))
            .ForPath(cart => cart.Product!.Price,
            opt => opt.MapFrom(p => p.Price))
            .ForPath(cart => cart.Amount,
            opt => opt.MapFrom(p => p.Quantity))
            .ReverseMap();
    }
}