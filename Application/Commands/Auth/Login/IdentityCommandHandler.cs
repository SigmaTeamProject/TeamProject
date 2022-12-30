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
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Auth.Login
{
    public class IdentityCommandHandler : IRequestHandler<LoginCommand, (CustomerModel, string)>
    {
        private readonly IRepository<Customer> _repository;
        private readonly IMapper _mapper;

        public IdentityCommandHandler(IMapper mapper, IRepository<Customer> repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<(CustomerModel, string)> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.Query().FirstOrDefaultAsync(customer =>
                customer.Login == request.Login && customer.Password == request.Password);
        
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, customer!.Id.ToString()),
                new Claim(ClaimTypes.Role, customer.Role!)
            };
            var credentials = new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = AuthOptions.ISSUER,
                Audience = AuthOptions.AUDIENCE,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(60),
                SigningCredentials = credentials
            };
        
            var tokenHandler = new JwtSecurityTokenHandler();
        
            var token = tokenHandler.CreateToken(tokenDescriptor);
        
            return (_mapper.Map<CustomerModel>(customer), tokenHandler.WriteToken(token));
        }
    }
}