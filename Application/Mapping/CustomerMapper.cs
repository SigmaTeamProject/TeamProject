using Application.Commands.Auth.Registration;
using Application.Models;
using AutoMapper;
using Data;

namespace Application.Mapping;

public class CustomerMapper : Profile
{
    public CustomerMapper()
    {
        CreateMap<Customer, RegisterUserCommand>().ReverseMap();
        CreateMap<Customer, CustomerModel>().ReverseMap();
    }
}