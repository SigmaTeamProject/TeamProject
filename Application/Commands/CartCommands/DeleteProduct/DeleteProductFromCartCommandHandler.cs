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
        private readonly IRepository<CartItem> _cartItemRepository;
        private readonly IMapper _mapper;

        public DeleteProductFromCartCommandHandler(IMapper mapper, IRepository<Cart> cartRepository, 
            IRepository<CartItem> cartItemRepository)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
        }

        public async Task<CartModel> Handle(DeleteProductFromCartCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentException();
            var userId = request.UserId;
            var cart = await _cartRepository.Query()
                .Include(cart => cart.Items)
                .FirstOrDefaultAsync(cart => cart.CustomerId == userId);

            if(cart == null || cart.Items == null) throw new ArgumentException();

            var itemToDelete = cart.Items.FirstOrDefault(item => item.ProductId == request.ProductId);

            if (itemToDelete != null) cart.Items.Remove(itemToDelete);

            await _cartRepository.UpdateAsync(cart);
            await _cartRepository.SaveChangesAsync();

            var list = _cartItemRepository.Query()
                .Include(item => item.Product)
                .Where(item => cart.Items.Contains(item));
            var cartModel = new CartModel
            {
                Products = _mapper.Map<ICollection<BuyProductModel>>(list)
            };
            cartModel.TotalPrice = cartModel.Products.Select(p => p.Price * p.Quantity).Sum();

            return cartModel;
        }
    }
}
