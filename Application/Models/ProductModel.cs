using AutoMapper;
using Data;

namespace Application.Models;

public class ProductModel
{
    public decimal Price { get; set; }
    public double Weight { get; set; }
    public string Name { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ProductModel>();
    }
}