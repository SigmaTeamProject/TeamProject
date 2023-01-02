using Application.Exceptions;
using Application.Models;
using AutoMapper;
using DAL.Repositry;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.CartCommands.DeleteProduct
{
    public class DeleteProductFromCartCommandHandler : IRequestHandler<DeleteProductFromCartCommand, CartModel>
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IMapper _mapper;

        public DeleteProductFromCartCommandHandler(IMapper mapper, IRepository<Cart> cartRepository)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<CartModel> Handle(DeleteProductFromCartCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentException();
            var userId = request.UserId;
            var cart = await _cartRepository.Query()
                .Include(cart => cart.Items)
                .ThenInclude(items => items.Product)
                .FirstOrDefaultAsync(cart => cart.CustomerId == userId);

            if(cart == null || cart.Items == null) throw new ArgumentException();

            var itemToDelete = cart.Items.FirstOrDefault(item => item.ProductId == request.ProductId);

            if (itemToDelete == null) throw new NotFoundException(nameof(cart.Items), request.ProductId);
                
            cart.Items.Remove(itemToDelete);

            await _cartRepository.UpdateAsync(cart);
            await _cartRepository.SaveChangesAsync();

            var cartModel = new CartModel
            {
                Products = await _mapper.ProjectTo<BuyProductModel>(cart.Items.AsQueryable()).ToListAsync()
            };
            cartModel.TotalPrice = cartModel.Products.Sum(p => p.Price * p.Quantity);

            return cartModel;
        }
    }
}
