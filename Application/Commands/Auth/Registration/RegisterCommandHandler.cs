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

namespace Application.Commands.Auth.Registration
{
    public class RegisterCommandHandler : IRequestHandler<RegisterUserCommand, (CustomerModel, string)>
    {
        private readonly IRepository<Customer> _repository;
        private readonly IMapper _mapper;
        private readonly SymmetricSecurityKey _key;

        public RegisterCommandHandler(IRepository<Customer> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("adsf"));
        }

        public async Task<(CustomerModel, string)> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Password != request.ConfirmPassword)
            {
                throw new Exception("Passwords don't match");
            }

            if (request.BirthDate == null)
            {
                request.BirthDate = DateOnly.MaxValue;
            }
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        
            request.Password = passwordHash;
        
            var customer = await _repository.AddAsync(_mapper.Map<Customer>(request));
            customer.Role = "Customer";
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, customer.Login),
                new Claim(ClaimTypes.Role, customer.Role!)
            };
            
        
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(60)
            };
        
            var tokenHandler = new JwtSecurityTokenHandler();
        
            var token = tokenHandler.CreateToken(tokenDescriptor);
            await _repository.SaveChangesAsync();
            return (_mapper.Map<CustomerModel>(customer), tokenHandler.WriteToken(token));
        }
    }
}
