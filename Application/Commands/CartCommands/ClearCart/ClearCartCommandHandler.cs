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

        public ClearCartCommandHandler(IRepository<Cart> cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<CartModel> Handle(ClearCartCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentException();
            var userId = request.UserId;
            var cart = await _cartRepository.Query()
                .Include(cart => cart.Items)
                .FirstOrDefaultAsync(cart => cart.CustomerId == userId);

            if (cart == null || cart.Items == null) throw new ArgumentException();
            
            cart.Items.Clear();

            await _cartRepository.UpdateAsync(cart);
            await _cartRepository.SaveChangesAsync();

            return new CartModel();
        }
    }
}
