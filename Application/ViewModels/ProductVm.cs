using Application.Mapping;
using AutoMapper;
using Data;

namespace Application.ViewModels
{
    public class ProductVm : IMapWith<Product>
    {
        public decimal Price { get; set; }
        public double Weight { get; set; }
        public string Name { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductVm>();
        }
    }
}
