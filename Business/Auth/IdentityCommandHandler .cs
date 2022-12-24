using AutoMapper;
using Data;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Persistence.Context;
using System.Data.Entity.Core.EntityClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Business.Auth
{
    public class IdentityCommandHandler : IRequestHandler<LoginCommand, (CustomerModel, string)>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public IdentityCommandHandler(ApplicationDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(customer =>
                customer.Login == request.Login && customer.Password == request.Password, cancellationToken);
            
            if (customer == null)
                throw new UserAccessDeniedExceptions($"Login: {request.Login}, password: {request.Password}");
            
            var claims = new List<Claim>
            {
                new (ClaimsIdentity.DefaultNameClaimType, customer.Login),
                new (ClaimsIdentity.DefaultRoleClaimType, customer.Role)
            };

            var now = DateTime.Now;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            
            return (encodedJwt, customer);
        }
    }
}
