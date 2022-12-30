using Application.Models;
using AutoMapper;
using DAL.Repositry;
using Data;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Commands.Auth.JWT;

namespace Application.Commands.Auth.Registration
{
    public class RegisterCommandHandler : IRequestHandler<RegisterUserCommand, (CustomerModel, string)>
    {
        private readonly IRepository<Customer> _repository;
        private readonly IMapper _mapper;
        private readonly ITokenManager _tokenManager;

        public RegisterCommandHandler(IRepository<Customer> repository, IMapper mapper, ITokenManager tokenManager)
        {
            _repository = repository;
            _mapper = mapper;
            _tokenManager = tokenManager;
        }

        public async Task<(CustomerModel, string)> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Password != request.ConfirmPassword)
            {
                throw new Exception("Passwords don't match");
            }

            if (request.BirthDate == null)
            {
                request.BirthDate = DateOnly.Parse("2022-01-01");
            }
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            request.Password = passwordHash;
            var customer = await _repository.AddAsync(_mapper.Map<Customer>(request));
            customer.Role = "Customer";
            
            await _repository.SaveChangesAsync();
            return (_mapper.Map<CustomerModel>(customer), _tokenManager.GenerateToken(customer));
        }
    }
}
