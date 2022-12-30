using Application.Commands.Auth.Login;
using Application.Commands.Auth.Registration;
using Application.Dtos;
using AutoMapper;

namespace Application.Mapping;

public class AuthMapper : Profile
{
    public AuthMapper()
    {
        CreateMap<LoginDto, LoginCommand>().ReverseMap();
        CreateMap<RegisterUserDto, RegisterUserCommand>().ReverseMap();
    }
}