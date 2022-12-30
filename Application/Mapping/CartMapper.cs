using Application.Commands.CartCommands.AddProduct;
using Application.Dtos;
using AutoMapper;

namespace Application.Mapping;

public class CartMapper : Profile
{
    public CartMapper()
    {
        CreateMap<AddInCartProductDto, AddProductInCartCommand>();
    }
}