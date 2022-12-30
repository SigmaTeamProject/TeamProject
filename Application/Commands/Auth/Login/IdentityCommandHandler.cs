using Application.Models;
using AutoMapper;
using Data;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Extensions;
using DAL.Repositry;
using Application.Commands.Auth.JWT;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Auth.Login
{
    public class IdentityCommandHandler : IRequestHandler<LoginCommand, (CustomerModel, string)>
    {
        private readonly IRepository<Customer> _repository;
        private readonly IMapper _mapper;
        private readonly ITokenManager _tokenManager;

        public IdentityCommandHandler(IMapper mapper, IRepository<Customer> repository, 
            ITokenManager tokenManager)
        {
            _repository = repository;
            _mapper = mapper;
            _tokenManager = tokenManager;
        }

        public async Task<(CustomerModel, string)> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.FirstOrDefaultAsync(customer =>
                customer.Login == request.Login);

            if (customer == null)
            {
                return (new CustomerModel(), string.Empty);
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, customer.Password))
            {
                return (new CustomerModel(), string.Empty);
            }

            var token = _tokenManager.GenerateToken(customer);
        
            return (_mapper.Map<CustomerModel>(customer), token);
        }
    }
}