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
        CreateMap<Cart, CartModel>().ReverseMap();
        CreateMap<BuyProductModel, StorageItem>().ReverseMap();
        CreateMap<Product, ProductModel>().ReverseMap();
    }
}