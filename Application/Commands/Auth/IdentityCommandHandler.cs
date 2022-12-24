using Application.Models;
using AutoMapper;
using Data;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Persistence.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Auth
{
    public class IdentityCommandHandler : IRequestHandler<LoginCommand, (CustomerModel, string)>
    {
        private readonly IRepository<Customer> _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public IdentityCommandHandler(IMapper mapper, IRepository<Customer> repository, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _config = configuration;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]!));
        }

        public async Task<(CustomerModel, string)> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.FirstOrDefault(customer =>
            customer.Login == request.Login && customer.Password == request.Password);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, customer.Login),
                new Claim(ClaimTypes.Role, customer.Roles.First().ToString())
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