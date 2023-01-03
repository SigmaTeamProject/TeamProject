using Application.Models;
using AutoMapper;
using DAL.Repositry;
using Data;
using MediatR;
using Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Auth.Registration
{
    public class RegisterCommandHandler : IRequestHandler<RegisterUserCommand, (CustomerModel, string)>
    {
        private readonly IRepository<Customer> _repository;
        private readonly IRepository<Cart> _cartRepository;
        private readonly IMapper _mapper;
        private readonly ITokenManager _tokenManager;
        private readonly IModeratorService _moderatorService;

        public RegisterCommandHandler(IRepository<Customer> repository, IRepository<Cart> cartRepository,
            IMapper mapper, ITokenManager tokenManager, IModeratorService moderatorService)
        {
            _repository = repository;
            _cartRepository = cartRepository;
            _mapper = mapper;
            _tokenManager = tokenManager;
            _moderatorService = moderatorService;
        }

        public async Task<(CustomerModel, string)> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Password != request.ConfirmPassword)
            {
                throw new Exception("Passwords don't match");
            }
            if (await IsRegister(request.Login))
            {
                throw new ArgumentException("User with this id already registered!");
            }

            var role = "Customer";
            if (request.Token != null)
            {
                if (_moderatorService.Verify(request.Token))
                {
                    role = "Moderator";
                }
            }
            request.BirthDate ??= DateOnly.Parse("2022-01-01");
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            request.Password = passwordHash;
            var mc = _mapper.Map<Customer>(request);
            mc.BirthDay = request.BirthDate.Value;
            var customer = await _repository.AddAsync(mc);
            customer.Role = role;
            await _repository.SaveChangesAsync();

            await _cartRepository.AddAsync(new Cart() { CustomerId = customer.Id});
            await _cartRepository.SaveChangesAsync();
            return (_mapper.Map<CustomerModel>(customer), _tokenManager.GenerateToken(customer));
        }

        private async Task<bool> IsRegister(string login)
        {
            var customer = await _repository.Query()
                .FirstOrDefaultAsync(customer => customer.Login == login);
            return customer != null;
        }
    }
}
