using Application.Commands.StoregeCommands.AddProduct;
using Application.Commands.StoregeCommands.UpdateProduct;
using Application.Dtos;
using Application.Models;
using AutoMapper;
using Data;

namespace Application.Mapping
{
    public class StorageMapper : Profile
    {
        public StorageMapper()
        {
            CreateMap<StorageItemDto, AddProductInStorageCommand>().ReverseMap();
            CreateMap<UpdateStorageItemDto, UpdateProductInStorageCommand>().ReverseMap();
            CreateMap<AddProductInStorageCommand, StorageItem>().ReverseMap();
            CreateMap<ProductCharacteristicModel, ProductCharacteristic>().ReverseMap();
            CreateMap<StorageItemModel, StorageItem>()
                .ForPath(cart => cart.Product!.Name,
                opt => opt.MapFrom(p => p.Name))
                .ForPath(cart => cart.Product!.Price,
                opt => opt.MapFrom(p => p.Price))
                .ForPath(cart => cart.Amount,
                opt => opt.MapFrom(p => p.Amount))
                .ForPath(cart => cart.Product!.Characteristics,
                opt => opt.MapFrom(p => p.Characteristics))
                .ReverseMap();
        }
    }
}
