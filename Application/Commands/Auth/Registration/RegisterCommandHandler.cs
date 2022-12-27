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
        private readonly IConfiguration _config;

        public RegisterCommandHandler(IRepository<Customer> repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _config = configuration;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]!));
        }

        public async Task<(CustomerModel, string)> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Password != request.ConfirmPassword)
            {
                throw new Exception("Passwords don't match");
            }
        
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        
            request.Password = passwordHash;
        
            var customer = await _repository.AddAsync(_mapper.Map<Customer>(request));
        
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, customer.Login),
                new Claim(ClaimTypes.Role, customer.Roles.First().ToString()!)
            };
        
            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
        
            var tokenDescriptor = new SecurityTokenDescriptor
            {
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
