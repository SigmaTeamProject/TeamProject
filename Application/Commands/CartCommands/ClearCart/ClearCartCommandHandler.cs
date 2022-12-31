using Application.Models;
using AutoMapper;
using DAL.Repositry;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.CartCommands.ClearCart
{
    public class ClearCartCommandHandler : IRequestHandler<ClearCartCommand, CartModel>
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IMapper _mapper;

        public ClearCartCommandHandler(IMapper mapper, IRepository<Cart> cartRepository)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<CartModel> Handle(ClearCartCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentException();
            var userId = request.UserId;
            var cart = await _cartRepository.Query()
                .Include(cart => cart.Customer)
                .Include(cart => cart.Items)
                .FirstOrDefaultAsync(cart => cart.Customer!.Id == userId);

            if (cart.Items != null) cart.Items.Clear();

            await _cartRepository.UpdateAsync(cart);
            await _cartRepository.SaveChangesAsync();

            return _mapper.Map<CartModel>(cart);
        }
    }
}
